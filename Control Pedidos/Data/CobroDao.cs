using System;
using System.Collections.Generic;
using System.Globalization;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    public class CobroDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public CobroDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public decimal ObtenerSaldoCliente(int clienteId)
        {
            const string query = @"SELECT IFNULL(SUM(f_totalPedido(p.pedido_id) - f_cobroPedido(p.pedido_id)), 0) AS saldo
                                    FROM banquetes.pedidos p
                                    WHERE p.cliente_id = @clienteId AND p.estatus = 'N';";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@clienteId", clienteId);
                connection.Open();
                var result = command.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result, CultureInfo.InvariantCulture);
            }
        }

        public IList<PedidoSaldo> ObtenerPedidosConSaldo(int clienteId)
        {
            const string query = @"SELECT
                                        p.pedido_id,
                                        p.folio,
                                        p.fecha_entrega,
                                        ev.tiene_serie,
                                        ev.serie,
                                        f_totalPedido(p.pedido_id) AS total,
                                        f_cobroPedido(p.pedido_id) AS abonado
                                    FROM banquetes.pedidos p
                                    LEFT JOIN banquetes.eventos ev ON ev.evento_id = p.evento_id
                                    WHERE p.cliente_id = @clienteId AND p.estatus = 'N'
                                    ORDER BY p.fecha_entrega ASC, p.pedido_id ASC;";

            var pedidos = new List<PedidoSaldo>();

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@clienteId", clienteId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedidoId = reader.GetInt32("pedido_id");
                        var folio = reader.IsDBNull(reader.GetOrdinal("folio")) ? (int?)null : reader.GetInt32("folio");
                        var tieneSerie = !reader.IsDBNull(reader.GetOrdinal("tiene_serie")) && reader.GetBoolean("tiene_serie");
                        var serie = reader.IsDBNull(reader.GetOrdinal("serie")) ? string.Empty : reader.GetString("serie");
                        var total = reader.IsDBNull(reader.GetOrdinal("total")) ? 0m : reader.GetDecimal("total");
                        var abonado = reader.IsDBNull(reader.GetOrdinal("abonado")) ? 0m : reader.GetDecimal("abonado");

                        var folioFormateado = FormatearFolio(folio, tieneSerie ? serie : string.Empty, pedidoId);

                        pedidos.Add(new PedidoSaldo
                        {
                            PedidoId = pedidoId,
                            Folio = folioFormateado,
                            FechaEntrega = reader.IsDBNull(reader.GetOrdinal("fecha_entrega"))
                                ? DateTime.MinValue
                                : reader.GetDateTime("fecha_entrega"),
                            Total = total,
                            Abonado = abonado,
                            MontoAsignado = 0m
                        });
                    }
                }
            }

            return pedidos;
        }

        public IList<FormaCobro> ObtenerFormasCobro()
        {
            const string query = @"SELECT forma_cobro_id, nombre, estatus
                                    FROM banquetes.formas_cobros
                                    ORDER BY nombre;";

            var formas = new List<FormaCobro>();

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var estatus = reader.IsDBNull(reader.GetOrdinal("estatus")) ? string.Empty : reader.GetString("estatus");
                        var activo = string.IsNullOrWhiteSpace(estatus)
                            || string.Equals(estatus, "N", StringComparison.OrdinalIgnoreCase)
                            || string.Equals(estatus, "A", StringComparison.OrdinalIgnoreCase)
                            || string.Equals(estatus, "S", StringComparison.OrdinalIgnoreCase);

                        formas.Add(new FormaCobro
                        {
                            Id = reader.GetInt32("forma_cobro_id"),
                            Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                            Activo = activo
                        });
                    }
                }
            }

            return formas;
        }

        public bool RegistrarCobro(Cobro cobro, IList<CobroDetalle> detalles, out string message)
        {
            if (cobro == null)
            {
                throw new ArgumentNullException(nameof(cobro));
            }

            if (detalles == null)
            {
                throw new ArgumentNullException(nameof(detalles));
            }

            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var cobroId = InsertarCobro(cobro, connection, transaction);
                        InsertarDetalles(cobroId, detalles, connection, transaction);
                        transaction.Commit();
                        cobro.Id = cobroId;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"No se pudo registrar el cobro: {ex.Message}";
                return false;
            }
        }

        private int InsertarCobro(Cobro cobro, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string insertSql = @"INSERT INTO banquetes.cobros_pedidos
                                        (usuario_id, empresa_id, cliente_id, forma_cobro_id, monto, fecha, fecha_creacion, estatus)
                                        VALUES (@usuarioId, @empresaId, @clienteId, @formaCobroId, @monto, NOW(), NOW(), 'N');";

            using (var command = new MySqlCommand(insertSql, connection, transaction))
            {
                command.Parameters.AddWithValue("@usuarioId", cobro.UsuarioId);
                command.Parameters.AddWithValue("@empresaId", cobro.EmpresaId);
                command.Parameters.AddWithValue("@clienteId", cobro.ClienteId);
                command.Parameters.AddWithValue("@formaCobroId", cobro.FormaCobroId);
                command.Parameters.AddWithValue("@monto", cobro.Monto);

                command.ExecuteNonQuery();
                cobro.Fecha = DateTime.Now;
                cobro.FechaCreacion = cobro.Fecha;
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        private static void InsertarDetalles(int cobroId, IEnumerable<CobroDetalle> detalles, MySqlConnection connection, MySqlTransaction transaction)
        {
            const string insertDetalleSql = @"INSERT INTO banquetes.cobros_pedidos_det (cobro_pedido_id, pedido_id, monto)
                                              VALUES (@cobroId, @pedidoId, @monto);";

            using (var command = new MySqlCommand(insertDetalleSql, connection, transaction))
            {
                command.Parameters.Add("@cobroId", MySqlDbType.Int32);
                command.Parameters.Add("@pedidoId", MySqlDbType.Int32);
                command.Parameters.Add("@monto", MySqlDbType.Decimal);

                foreach (var detalle in detalles)
                {
                    if (detalle == null || detalle.Monto <= 0)
                    {
                        continue;
                    }

                    command.Parameters["@cobroId"].Value = cobroId;
                    command.Parameters["@pedidoId"].Value = detalle.PedidoId;
                    command.Parameters["@monto"].Value = detalle.Monto;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static string FormatearFolio(int? folio, string serie, int pedidoId)
        {
            if (folio.HasValue)
            {
                var numero = folio.Value.ToString("D4", CultureInfo.InvariantCulture);
                return string.IsNullOrWhiteSpace(serie) ? numero : string.Concat(serie, numero);
            }

            return $"PED{pedidoId:D5}";
        }
    }
}

using System;
using System.Collections.Generic;
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
            const string query = @"SELECT IFNULL(SUM(f_totalPedido(p.pedido_id) - f_cobroPedido(p.pedido_id)), 0)
FROM banquetes.pedidos p
WHERE p.cliente_id = @clienteId
  AND p.estatus = 'N';";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@clienteId", clienteId);
                connection.Open();
                var result = command.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
            }
        }

        public List<PedidoSaldo> ObtenerPedidosConSaldo(int clienteId)
        {
            const string query = @"SELECT
    p.pedido_id,
    COALESCE(CAST(p.folio AS CHAR), CAST(p.pedido_id AS CHAR)) AS folio,
    p.fecha_entrega,
    IFNULL(f_totalPedido(p.pedido_id), 0) AS total,
    IFNULL(f_cobroPedido(p.pedido_id), 0) AS abonado
FROM banquetes.pedidos p
WHERE p.cliente_id = @clienteId
  AND p.estatus = 'N'
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
                        var total = reader.GetDecimal("total");
                        var abonado = reader.GetDecimal("abonado");
                        if (total <= 0)
                        {
                            continue;
                        }

                        var saldo = total - abonado;
                        if (saldo <= 0)
                        {
                            continue;
                        }

                        var fechaEntregaOrdinal = reader.GetOrdinal("fecha_entrega");
                        var fechaEntrega = reader.IsDBNull(fechaEntregaOrdinal)
                            ? DateTime.Now
                            : reader.GetDateTime(fechaEntregaOrdinal);

                        pedidos.Add(new PedidoSaldo
                        {
                            PedidoId = reader.GetInt32("pedido_id"),
                            Folio = reader.GetString("folio"),
                            FechaEntrega = fechaEntrega,
                            Total = total,
                            Abonado = abonado
                        });
                    }
                }
            }

            return pedidos;
        }

        public List<FormaCobro> ObtenerFormasCobro()
        {
            const string query = @"SELECT forma_cobro_id, nombre, IFNULL(descripcion, '') AS descripcion
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
                        formas.Add(new FormaCobro
                        {
                            Id = reader.GetInt32("forma_cobro_id"),
                            Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion")
                        });
                    }
                }
            }

            return formas;
        }

        public bool RegistrarCobro(Cobro cobro, List<CobroDetalle> detalles, out string message)
        {
            if (cobro == null)
            {
                throw new ArgumentNullException(nameof(cobro));
            }

            if (detalles == null)
            {
                throw new ArgumentNullException(nameof(detalles));
            }

            if (detalles.Count == 0)
            {
                message = "Seleccione al menos un pedido para aplicar el abono.";
                return false;
            }

            if (cobro.Monto <= 0)
            {
                message = "El monto del abono debe ser mayor a cero.";
                return false;
            }

            message = string.Empty;

            var totalDetalles = 0m;
            foreach (var detalle in detalles)
            {
                if (detalle.Monto < 0)
                {
                    message = "Los montos asignados no pueden ser negativos.";
                    return false;
                }

                totalDetalles += detalle.Monto;
            }

            if (Math.Abs(totalDetalles - cobro.Monto) > 0.01m)
            {
                message = "El monto total del abono no coincide con la suma de los pedidos seleccionados.";
                return false;
            }

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCobroSql = @"INSERT INTO banquetes.cobros_pedidos
    (usuario_id, empresa_id, cliente_id, forma_cobro_id, monto, fecha, fecha_creacion, estatus)
VALUES
    (@usuarioId, @empresaId, @clienteId, @formaCobroId, @monto, NOW(), NOW(), 'N');";

                        long cobroId;
                        using (var command = new MySqlCommand(insertCobroSql, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@usuarioId", cobro.UsuarioId);
                            command.Parameters.AddWithValue("@empresaId", cobro.EmpresaId);
                            command.Parameters.AddWithValue("@clienteId", cobro.ClienteId);
                            command.Parameters.AddWithValue("@formaCobroId", cobro.FormaCobroId);
                            command.Parameters.AddWithValue("@monto", cobro.Monto);
                            command.ExecuteNonQuery();
                            cobroId = command.LastInsertedId;
                        }

                        var insertDetalleSql = @"INSERT INTO banquetes.cobros_pedidos_det
    (cobro_pedido_id, pedido_id, monto)
VALUES
    (@cobroId, @pedidoId, @monto);";

                        foreach (var detalle in detalles)
                        {
                            using (var command = new MySqlCommand(insertDetalleSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@cobroId", cobroId);
                                command.Parameters.AddWithValue("@pedidoId", detalle.PedidoId);
                                command.Parameters.AddWithValue("@monto", detalle.Monto);
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        cobro.Id = (int)cobroId;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"No se pudo registrar el abono: {ex.Message}";
                return false;
            }
        }

        public void ActualizarEstadoImpresion(int cobroId, bool impreso)
        {
            const string query = "UPDATE banquetes.cobros_pedidos SET impreso = @impreso WHERE cobro_pedido_id = @cobroId;";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@impreso", impreso ? "S" : "N");
                    command.Parameters.AddWithValue("@cobroId", cobroId);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {
                // El campo impreso es opcional; si no existe, ignoramos el error para no interrumpir el flujo.
            }
        }
    }
}

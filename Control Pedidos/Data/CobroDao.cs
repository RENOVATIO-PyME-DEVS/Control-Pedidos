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
    COALESCE(CAST(f_folio_pedido(p.pedido_id) AS CHAR), CAST(p.pedido_id AS CHAR)) AS folio,
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
            const string query = @"SELECT forma_cobro_id, nombre,IFNULL(tipo_cobro, '') AS descripcion, tipo
                                    FROM banquetes.formas_cobros
                                   -- ORDER BY nombre;";

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
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion"),
                            Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? string.Empty : reader.GetString("tipo")
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
    (usuario_id, empresa_id, cliente_id, forma_cobro_id, monto, fecha, fecha_creacion, estatus, impreso)
VALUES
    (@usuarioId, @empresaId, @clienteId, @formaCobroId, @monto, NOW(), NOW(), 'N', 'N');";

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
                        cobro.Impreso = "N";
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

        public bool MarcarCobroImpreso(int cobroId, bool correcto)
        {
            const string query = @"UPDATE banquetes.cobros_pedidos
SET impreso = @impreso
WHERE cobro_pedido = @cobroId;";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@impreso", correcto ? "S" : "N");
                command.Parameters.AddWithValue("@cobroId", cobroId);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Cobro> ObtenerCobrosPorCliente(int clienteId)
        {
            const string query = @"SELECT
    cp.cobro_pedido,
    IFNULL(cp.cobro_pedido, cp.cobro_pedido) AS cobro_pedido,
    cp.cliente_id,
    cp.forma_cobro_id,
    IFNULL(fc.nombre, '') AS forma_cobro,
    cp.monto,
    cp.fecha,
    cp.estatus,
    IFNULL(cp.impreso, 'N') AS impreso
FROM banquetes.cobros_pedidos cp
LEFT JOIN banquetes.formas_cobros fc ON fc.forma_cobro_id = cp.forma_cobro_id
WHERE cp.cliente_id = @clienteId
ORDER BY cp.fecha DESC;";

            var cobros = new List<Cobro>();

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@clienteId", clienteId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fechaOrdinal = reader.GetOrdinal("fecha");
                        var cobro = new Cobro
                        {
                            CobroPedidoId = reader.GetInt32("cobro_pedido"),
                            ClienteId = reader.GetInt32("cliente_id"),
                            FormaCobroId = reader.IsDBNull(reader.GetOrdinal("forma_cobro_id")) ? 0 : reader.GetInt32("forma_cobro_id"),
                            FormaCobroNombre = reader.GetString("forma_cobro"),
                            Monto = reader.GetDecimal("monto"),
                            Fecha = reader.IsDBNull(fechaOrdinal) ? DateTime.Now : reader.GetDateTime(fechaOrdinal),
                            Estatus = reader.IsDBNull(reader.GetOrdinal("estatus")) ? string.Empty : reader.GetString("estatus"),
                            Impreso = reader.IsDBNull(reader.GetOrdinal("impreso")) ? "N" : reader.GetString("impreso")
                        };

                        cobros.Add(cobro);
                    }
                }
            }

            return cobros;
        }

        public Cobro ObtenerCobroPorId(int cobroId)
        {
            const string cobroQuery = @"SELECT
    cp.cobro_pedido,
    IFNULL(cp.cobro_pedido, cp.cobro_pedido) AS cobro_pedido,
    cp.usuario_id,
    cp.empresa_id,
    cp.cliente_id,
    cp.forma_cobro_id,
    cp.monto,
    cp.fecha,
    cp.fecha_creacion,
    cp.estatus,
    IFNULL(cp.impreso, 'N') AS impreso,
    IFNULL(fc.nombre, '') AS forma_cobro,
    c.cliente_id,
    c.nombre AS cliente_nombre,
    IFNULL(c.rfc, '') AS cliente_rfc,
    IFNULL(c.correo, '') AS cliente_correo,
    IFNULL(c.telefono, '') AS cliente_telefono,
    e.empresa_id,
    e.nombre AS empresa_nombre,
    IFNULL(e.rfc, '') AS empresa_rfc,
    IFNULL(e.telefono, '') AS empresa_telefono
FROM banquetes.cobros_pedidos cp
LEFT JOIN banquetes.formas_cobros fc ON fc.forma_cobro_id = cp.forma_cobro_id
LEFT JOIN banquetes.clientes c ON c.cliente_id = cp.cliente_id
LEFT JOIN banquetes.empresas e ON e.empresa_id = cp.empresa_id
WHERE cp.cobro_pedido = @cobroId;";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(cobroQuery, connection))
            {
                command.Parameters.AddWithValue("@cobroId", cobroId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var fechaOrdinal = reader.GetOrdinal("fecha");
                    var fechaCreacionOrdinal = reader.GetOrdinal("fecha_creacion");
                    var cobro = new Cobro
                    {
                        CobroPedidoId = reader.GetInt32("cobro_pedido"),
                        UsuarioId = reader.GetInt32("usuario_id"),
                        EmpresaId = reader.GetInt32("empresa_id"),
                        ClienteId = reader.GetInt32("cliente_id"),
                        FormaCobroId = reader.IsDBNull(reader.GetOrdinal("forma_cobro_id")) ? 0 : reader.GetInt32("forma_cobro_id"),
                        FormaCobroNombre = reader.GetString("forma_cobro"),
                        Monto = reader.GetDecimal("monto"),
                        Fecha = reader.IsDBNull(fechaOrdinal) ? DateTime.Now : reader.GetDateTime(fechaOrdinal),
                        FechaCreacion = reader.IsDBNull(fechaCreacionOrdinal) ? DateTime.Now : reader.GetDateTime(fechaCreacionOrdinal),
                        Estatus = reader.IsDBNull(reader.GetOrdinal("estatus")) ? string.Empty : reader.GetString("estatus"),
                        Impreso = reader.IsDBNull(reader.GetOrdinal("impreso")) ? "N" : reader.GetString("impreso"),
                        Cliente = new Cliente
                        {
                            Id = reader.GetInt32("cliente_id"),
                            Nombre = reader.IsDBNull(reader.GetOrdinal("cliente_nombre")) ? string.Empty : reader.GetString("cliente_nombre"), 
                            Rfc = reader.IsDBNull(reader.GetOrdinal("cliente_rfc")) ? string.Empty : reader.GetString("cliente_rfc"),
                            Correo = reader.IsDBNull(reader.GetOrdinal("cliente_correo")) ? string.Empty : reader.GetString("cliente_correo"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("cliente_telefono")) ? string.Empty : reader.GetString("cliente_telefono")
                        },
                        Empresa = new Empresa
                        {
                            Id = reader.GetInt32("empresa_id"),
                            Nombre = reader.IsDBNull(reader.GetOrdinal("empresa_nombre")) ? string.Empty : reader.GetString("empresa_nombre"),
                            //Rfc = reader.IsDBNull(reader.GetOrdinal("empresa_rfc")) ? string.Empty : reader.GetString("empresa_rfc"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("empresa_telefono")) ? string.Empty : reader.GetString("empresa_telefono")
                        }
                    };

                    reader.Close();
                    cobro.Detalles = ObtenerDetallesCobro(connection, cobro.CobroPedidoId);
                    return cobro;
                }
            }
        }

        private IReadOnlyList<CobroDetalle> ObtenerDetallesCobro(MySqlConnection connection, int cobroId)
        {
            const string query = @"SELECT
    det.pedido_id,
    COALESCE(CAST(f_folio_pedido(det.pedido_id) AS CHAR), CAST(det.pedido_id AS CHAR)) AS folio,
    ped.fecha_entrega,
    det.monto
FROM banquetes.cobros_pedidos_det det
LEFT JOIN banquetes.pedidos ped ON ped.pedido_id = det.pedido_id
WHERE det.cobro_pedido_id = @cobroId;";

            var detalles = new List<CobroDetalle>();

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@cobroId", cobroId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var detalle = new CobroDetalle
                        {
                            PedidoId = reader.GetInt32("pedido_id"),
                            Folio = reader.IsDBNull(reader.GetOrdinal("folio")) ? string.Empty : reader.GetString("folio"),
                            FechaEntrega = reader.IsDBNull(reader.GetOrdinal("fecha_entrega")) ? DateTime.Now : reader.GetDateTime("fecha_entrega"),
                            Monto = reader.GetDecimal("monto")
                        };

                        detalles.Add(detalle);
                    }
                }
            }

            return detalles;
        }
    }
}

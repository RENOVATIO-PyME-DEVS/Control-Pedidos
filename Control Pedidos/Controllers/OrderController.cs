using System;
using System.Collections.Generic;
using System.Data;
using Control_Pedidos.Data;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Controllers
{
    public class OrderController
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public OrderController(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public DataTable GetOrderTable()
        {
            const string query = @"SELECT
    p.pedido_id AS Id,
    p.folio AS Folio,
    c.nombre AS Cliente,
    e.nombre AS Empresa,
    u.nombre AS Usuario,
    p.estatus AS Estatus,
    p.fecha AS Fecha,
    p.fecha_entrega AS FechaEntrega,
    p.hora_entrega AS HoraEntrega,
    p.requiere_factura AS RequiereFactura,
    p.notas AS Notas,
    IFNULL(det.TotalPedido, 0) AS Total,
    IFNULL(det.TotalPedido, 0) - IFNULL(cob.Cobrado, 0) AS SaldoPendiente
FROM pedidos p
INNER JOIN clientes c ON p.cliente_id = c.cliente_id
INNER JOIN empresas e ON p.empresa_id = e.empresa_id
INNER JOIN usuarios u ON p.usuario_id = u.usuario_id
LEFT JOIN (
    SELECT pedido_id, SUM(total) AS TotalPedido
    FROM pedidos_detalles
    GROUP BY pedido_id
) det ON det.pedido_id = p.pedido_id
LEFT JOIN (
    SELECT pedido_id, SUM(monto) AS Cobrado
    FROM cobros_pedidos_det
    GROUP BY pedido_id
) cob ON cob.pedido_id = p.pedido_id
ORDER BY p.fecha_creacion DESC";

            using (var connection = _connectionFactory.Create())
            using (var adapter = new MySqlDataAdapter(query, connection))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public IEnumerable<Pedido> GetOrders()
        {
            const string query = @"SELECT
    p.pedido_id AS PedidoId,
    p.folio,
    p.fecha,
    p.fecha_entrega,
    p.hora_entrega,
    p.requiere_factura,
    p.notas,
    p.fecha_creacion,
    p.estatus,
    p.cliente_id,
    c.nombre AS ClienteNombre,
    c.rfc AS ClienteRfc,
    c.correo AS ClienteCorreo,
    c.telefono AS ClienteTelefono,
    c.estatus AS ClienteEstatus,
    p.empresa_id,
    e.nombre AS EmpresaNombre,
    e.rfc AS EmpresaRfc,
    p.usuario_id,
    u.nombre AS UsuarioNombre,
    u.correo AS UsuarioCorreo,
    u.rol_usuario_id,
    u.estatus AS UsuarioEstatus,
    u.fecha_creacion AS UsuarioFechaCreacion,
    u.fecha_baja AS UsuarioFechaBaja,
    IFNULL(det.TotalPedido, 0) AS Total,
    IFNULL(cob.Cobrado, 0) AS Cobrado
FROM pedidos p
INNER JOIN clientes c ON p.cliente_id = c.cliente_id
INNER JOIN empresas e ON p.empresa_id = e.empresa_id
INNER JOIN usuarios u ON p.usuario_id = u.usuario_id
LEFT JOIN (
    SELECT pedido_id, SUM(total) AS TotalPedido
    FROM pedidos_detalles
    GROUP BY pedido_id
) det ON det.pedido_id = p.pedido_id
LEFT JOIN (
    SELECT pedido_id, SUM(monto) AS Cobrado
    FROM cobros_pedidos_det
    GROUP BY pedido_id
) cob ON cob.pedido_id = p.pedido_id
ORDER BY p.fecha_creacion DESC";

            var pedidos = new List<Pedido>();

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            Id = reader.GetInt32("PedidoId"),
                            Folio = reader.IsDBNull(reader.GetOrdinal("folio")) ? (int?)null : reader.GetInt32("folio"),
                            Fecha = reader.GetDateTime("fecha"),
                            FechaEntrega = reader.GetDateTime("fecha_entrega"),
                            HoraEntrega = reader.IsDBNull(reader.GetOrdinal("hora_entrega"))
                                ? (TimeSpan?)null
                                : reader.GetTimeSpan(reader.GetOrdinal("hora_entrega")),
                            RequiereFactura = string.Equals(reader.GetString("requiere_factura"), "S", StringComparison.OrdinalIgnoreCase),
                            Notas = reader.IsDBNull(reader.GetOrdinal("notas")) ? null : reader.GetString("notas"),
                            FechaCreacion = reader.GetDateTime("fecha_creacion"),
                            Estatus = reader.GetString("estatus"),
                            Total = reader.GetDecimal("Total"),
                            SaldoPendiente = reader.GetDecimal("Total") - reader.GetDecimal("Cobrado"),
                            Cliente = new Cliente
                            {
                                Id = reader.GetInt32("cliente_id"),
                                Nombre = reader.GetString("ClienteNombre"),
                                Rfc = reader.IsDBNull(reader.GetOrdinal("ClienteRfc")) ? null : reader.GetString("ClienteRfc"),
                                Correo = reader.IsDBNull(reader.GetOrdinal("ClienteCorreo")) ? null : reader.GetString("ClienteCorreo"),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("ClienteTelefono")) ? null : reader.GetString("ClienteTelefono"),
                                Estatus = reader.IsDBNull(reader.GetOrdinal("ClienteEstatus")) ? null : reader.GetString("ClienteEstatus")
                            },
                            Empresa = new Empresa
                            {
                                Id = reader.GetInt32("empresa_id"),
                                Nombre = reader.GetString("EmpresaNombre"),
                                Rfc = reader.GetString("EmpresaRfc")
                            },
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Nombre = reader.GetString("UsuarioNombre"),
                                Correo = reader.GetString("UsuarioCorreo"),
                                RolUsuarioId = reader.IsDBNull(reader.GetOrdinal("rol_usuario_id")) ? null : reader.GetInt32("rol_usuario_id"),
                                Estatus = reader.IsDBNull(reader.GetOrdinal("UsuarioEstatus")) ? string.Empty : reader.GetString("UsuarioEstatus"),
                                FechaCreacion = reader.IsDBNull(reader.GetOrdinal("UsuarioFechaCreacion")) ? (DateTime?)null : reader.GetDateTime("UsuarioFechaCreacion"),
                                FechaBaja = reader.IsDBNull(reader.GetOrdinal("UsuarioFechaBaja")) ? (DateTime?)null : reader.GetDateTime("UsuarioFechaBaja")
                            }
                        };

                        pedidos.Add(pedido);
                    }
                }
            }

            return pedidos;
        }

        public void CreateOrder(Pedido pedido)
        {
            const string query = @"INSERT INTO pedidos
    (usuario_id, empresa_id, cliente_id, folio, fecha, fecha_entrega, hora_entrega, requiere_factura, notas, fecha_creacion, estatus)
VALUES
    (@usuarioId, @empresaId, @clienteId, @folio, @fecha, @fechaEntrega, @horaEntrega, @requiereFactura, @notas, @fechaCreacion, @estatus)";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@usuarioId", pedido.Usuario.Id);
                command.Parameters.AddWithValue("@empresaId", pedido.Empresa.Id);
                command.Parameters.AddWithValue("@clienteId", pedido.Cliente.Id);
                command.Parameters.AddWithValue("@folio", (object?)pedido.Folio ?? DBNull.Value);
                command.Parameters.AddWithValue("@fecha", pedido.Fecha);
                command.Parameters.AddWithValue("@fechaEntrega", pedido.FechaEntrega);
                command.Parameters.AddWithValue("@horaEntrega", pedido.HoraEntrega.HasValue ? (object)pedido.HoraEntrega.Value : DBNull.Value);
                command.Parameters.AddWithValue("@requiereFactura", pedido.RequiereFactura ? "S" : "N");
                command.Parameters.AddWithValue("@notas", (object?)pedido.Notas ?? DBNull.Value);
                command.Parameters.AddWithValue("@fechaCreacion", pedido.FechaCreacion);
                command.Parameters.AddWithValue("@estatus", pedido.Estatus);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Pedido pedido)
        {
            const string query = @"UPDATE pedidos
SET usuario_id = @usuarioId,
    empresa_id = @empresaId,
    cliente_id = @clienteId,
    folio = @folio,
    fecha = @fecha,
    fecha_entrega = @fechaEntrega,
    hora_entrega = @horaEntrega,
    requiere_factura = @requiereFactura,
    notas = @notas,
    estatus = @estatus
WHERE pedido_id = @id";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@usuarioId", pedido.Usuario.Id);
                command.Parameters.AddWithValue("@empresaId", pedido.Empresa.Id);
                command.Parameters.AddWithValue("@clienteId", pedido.Cliente.Id);
                command.Parameters.AddWithValue("@folio", (object?)pedido.Folio ?? DBNull.Value);
                command.Parameters.AddWithValue("@fecha", pedido.Fecha);
                command.Parameters.AddWithValue("@fechaEntrega", pedido.FechaEntrega);
                command.Parameters.AddWithValue("@horaEntrega", pedido.HoraEntrega.HasValue ? (object)pedido.HoraEntrega.Value : DBNull.Value);
                command.Parameters.AddWithValue("@requiereFactura", pedido.RequiereFactura ? "S" : "N");
                command.Parameters.AddWithValue("@notas", (object?)pedido.Notas ?? DBNull.Value);
                command.Parameters.AddWithValue("@estatus", pedido.Estatus);
                command.Parameters.AddWithValue("@id", pedido.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int id)
        {
            const string query = "DELETE FROM pedidos WHERE pedido_id = @id";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

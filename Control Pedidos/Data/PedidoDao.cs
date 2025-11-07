using System;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    public class PedidoDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public PedidoDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool Agregar(Pedido pedido, out string message)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido));
            }

            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        if (!InsertPedido(pedido, connection, transaction, out message))
                        {
                            transaction.Rollback();
                            return false;
                        }

                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"No se pudo crear el pedido: {ex.Message}";
                return false;
            }
        }

        public bool ActualizarEstatus(int pedidoId, string nuevoEstatus, out string message, out string folioGenerado, out string folioFormateado)
        {
            if (pedidoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pedidoId));
            }

            if (string.IsNullOrWhiteSpace(nuevoEstatus))
            {
                throw new ArgumentException("El estatus es requerido", nameof(nuevoEstatus));
            }

            message = string.Empty;
            folioGenerado = string.Empty;
            folioFormateado = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        int? folioNumero = null;
                        string serie = string.Empty;
                        int? eventoId = null;
                        int empresaId = 0;

                        if (string.Equals(nuevoEstatus, "N", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var countCommand = new MySqlCommand("SELECT COUNT(*) FROM banquetes.pedidos_detalles WHERE pedido_id = @pedidoId;", connection, transaction))
                            {
                                countCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                                var detalles = Convert.ToInt32(countCommand.ExecuteScalar());
                                if (detalles == 0)
                                {
                                    message = "El pedido debe tener al menos un artículo para cerrarse.";
                                    transaction.Rollback();
                                    return false;
                                }
                            }

                            using (var pedidoCommand = new MySqlCommand(@"SELECT folio, empresa_id, evento_id
FROM banquetes.pedidos
WHERE pedido_id = @pedidoId
FOR UPDATE;", connection, transaction))
                            {
                                pedidoCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                                using (var reader = pedidoCommand.ExecuteReader())
                                {
                                    if (!reader.Read())
                                    {
                                        message = "No se encontró el pedido.";
                                        transaction.Rollback();
                                        return false;
                                    }

                                    empresaId = reader.GetInt32("empresa_id");
                                    eventoId = reader.IsDBNull(reader.GetOrdinal("evento_id")) ? (int?)null : reader.GetInt32("evento_id");

                                    if (!reader.IsDBNull(reader.GetOrdinal("folio")))
                                    {
                                        folioNumero = reader.GetInt32("folio");
                                    }
                                }
                            }

                            if (!folioNumero.HasValue)
                            {
                                var folioInfo = ObtenerFolioInfo(connection, transaction, empresaId, eventoId);
                                folioNumero = folioInfo.Numero;
                                serie = folioInfo.Serie;
                            }
                            else
                            {
                                serie = ObtenerSerieEvento(connection, transaction, eventoId);
                            }
                        }

                        var actualizarFolio = string.Equals(nuevoEstatus, "N", StringComparison.OrdinalIgnoreCase) && folioNumero.HasValue;
                        var updateSql = actualizarFolio
                            ? "UPDATE banquetes.pedidos SET estatus = @estatus, folio = @folio WHERE pedido_id = @pedidoId;"
                            : "UPDATE banquetes.pedidos SET estatus = @estatus WHERE pedido_id = @pedidoId;";

                        using (var command = new MySqlCommand(updateSql, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@estatus", nuevoEstatus);
                            command.Parameters.AddWithValue("@pedidoId", pedidoId);
                            if (actualizarFolio)
                            {
                                command.Parameters.AddWithValue("@folio", folioNumero.Value);
                            }
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        if (folioNumero.HasValue)
                        {
                            folioGenerado = folioNumero.Value.ToString();
                            folioFormateado = FormatearFolio(serie, folioNumero.Value);
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"No se pudo actualizar el estatus del pedido: {ex.Message}";
                return false;
            }
        }

        public bool AplicarDescuento(int pedidoId, decimal descuento, string usuarioCorreo, out string message, out string folioGenerado, out string folioFormateado)
        {
            if (pedidoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pedidoId));
            }

            if (string.IsNullOrWhiteSpace(usuarioCorreo))
            {
                throw new ArgumentException("El correo del usuario es requerido", nameof(usuarioCorreo));
            }

            message = string.Empty;
            folioGenerado = string.Empty;
            folioFormateado = string.Empty;

            if (descuento <= 0)
            {
                message = "El descuento debe ser mayor que cero.";
                return false;
            }

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        decimal totalPedido = 0;
                        using (var totalCommand = new MySqlCommand("SELECT SUM(cantidad * precio_unitario) FROM banquetes.pedidos_detalles WHERE pedido_id = @pedidoId;", connection, transaction))
                        {
                            totalCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                            var result = totalCommand.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                totalPedido = Convert.ToDecimal(result);
                            }
                        }

                        if (totalPedido <= 0)
                        {
                            message = "El pedido debe tener artículos registrados.";
                            transaction.Rollback();
                            return false;
                        }

                        if (descuento > totalPedido)
                        {
                            message = "El descuento no puede ser mayor que el total del pedido.";
                            transaction.Rollback();
                            return false;
                        }

                        int? folioNumero = null;
                        string serie = string.Empty;
                        int? eventoId = null;
                        int empresaId = 0;

                        using (var pedidoCommand = new MySqlCommand(@"SELECT folio, empresa_id, evento_id
FROM banquetes.pedidos
WHERE pedido_id = @pedidoId
FOR UPDATE;", connection, transaction))
                        {
                            pedidoCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                            using (var reader = pedidoCommand.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    message = "No se encontró el pedido.";
                                    transaction.Rollback();
                                    return false;
                                }

                                empresaId = reader.GetInt32("empresa_id");
                                eventoId = reader.IsDBNull(reader.GetOrdinal("evento_id")) ? (int?)null : reader.GetInt32("evento_id");

                                if (!reader.IsDBNull(reader.GetOrdinal("folio")))
                                {
                                    folioNumero = reader.GetInt32("folio");
                                }
                            }
                        }

                        if (!folioNumero.HasValue)
                        {
                            var folioInfo = ObtenerFolioInfo(connection, transaction, empresaId, eventoId);
                            folioNumero = folioInfo.Numero;
                            serie = folioInfo.Serie;
                        }
                        else
                        {
                            serie = ObtenerSerieEvento(connection, transaction, eventoId);
                        }

                        var updateSql = folioNumero.HasValue
                            ? "UPDATE banquetes.pedidos SET estatus = @estatus, folio = @folio, descuento = @descuento, usuario_descuento = @usuarioDescuento WHERE pedido_id = @pedidoId;"
                            : "UPDATE banquetes.pedidos SET estatus = @estatus, descuento = @descuento, usuario_descuento = @usuarioDescuento WHERE pedido_id = @pedidoId;";

                        using (var updateCommand = new MySqlCommand(updateSql, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@estatus", "N");
                            updateCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                            updateCommand.Parameters.AddWithValue("@descuento", descuento);
                            updateCommand.Parameters.AddWithValue("@usuarioDescuento", usuarioCorreo);

                            if (updateSql.Contains("@folio"))
                            {
                                updateCommand.Parameters.AddWithValue("@folio", folioNumero.Value);
                            }

                            updateCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        if (folioNumero.HasValue)
                        {
                            folioGenerado = folioNumero.Value.ToString();
                            folioFormateado = FormatearFolio(serie, folioNumero.Value);
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"No se pudo aplicar el descuento: {ex.Message}";
                folioGenerado = string.Empty;
                folioFormateado = string.Empty;
                return false;
            }
        }

        public string ObtenerFolio(int empresaId, int? eventoId)
        {
            using (var connection = _connectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var folioInfo = ObtenerFolioInfo(connection, transaction, empresaId, eventoId);
                        transaction.Commit();
                        return folioInfo.Formateado;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        internal bool InsertPedido(Pedido pedido, MySqlConnection connection, MySqlTransaction transaction, out string message)
        {
            message = string.Empty;

            var empresaId = pedido.Empresa?.Id ?? 0;
            if (empresaId <= 0)
            {
                message = "Seleccione una empresa válida.";
                return false;
            }

            if (pedido.Cliente == null || pedido.Cliente.Id <= 0)
            {
                message = "Seleccione un cliente válido.";
                return false;
            }

            if (pedido.Usuario == null || pedido.Usuario.Id <= 0)
            {
                message = "No se encontró el usuario que registra el pedido.";
                return false;
            }

            try
            {
                int? folioNumero = null;
                if (int.TryParse(pedido.Folio, out var folioExistente))
                {
                    folioNumero = folioExistente;
                }

                var folioFormateado = string.Empty;
                if (folioNumero.HasValue)
                {
                    var serie = pedido.Evento != null && pedido.Evento.TieneSerie ? pedido.Evento.Serie : string.Empty;
                    folioFormateado = FormatearFolio(serie, folioNumero.Value);
                }

                pedido.FechaCreacion = DateTime.Now;
                pedido.Estatus = string.IsNullOrEmpty(pedido.Estatus) ? "P" : pedido.Estatus;

                using (var command = new MySqlCommand(@"INSERT INTO banquetes.pedidos
(usuario_id, empresa_id, cliente_id, evento_id, folio, fecha, fecha_entrega, hora_entrega, requiere_factura, notas, fecha_creacion, estatus)
VALUES
(@usuarioId, @empresaId, @clienteId, @eventoId, @folio, @fecha, @fechaEntrega, @horaEntrega, @requiereFactura, @notas, @fechaCreacion, @estatus);", connection, transaction))
                {
                    command.Parameters.AddWithValue("@usuarioId", pedido.Usuario.Id);
                    command.Parameters.AddWithValue("@empresaId", empresaId);
                    command.Parameters.AddWithValue("@clienteId", pedido.Cliente.Id);
                    command.Parameters.AddWithValue("@eventoId", pedido.Evento != null && pedido.Evento.Id > 0 ? (object)pedido.Evento.Id : DBNull.Value);
                    if (folioNumero.HasValue)
                    {
                        command.Parameters.AddWithValue("@folio", folioNumero.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@folio", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@fecha", pedido.Fecha == default ? DateTime.Now : pedido.Fecha);
                    command.Parameters.AddWithValue("@fechaEntrega", pedido.FechaEntrega == default ? DateTime.Now : pedido.FechaEntrega);
                    command.Parameters.AddWithValue("@horaEntrega", pedido.HoraEntrega.HasValue ? (object)pedido.HoraEntrega.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@requiereFactura", pedido.RequiereFactura ? "S" : "N");
                    command.Parameters.AddWithValue("@notas", string.IsNullOrWhiteSpace(pedido.Notas) ? (object)DBNull.Value : pedido.Notas);
                    command.Parameters.AddWithValue("@fechaCreacion", pedido.FechaCreacion);
                    command.Parameters.AddWithValue("@estatus", pedido.Estatus);

                    command.ExecuteNonQuery();
                    pedido.Id = Convert.ToInt32(command.LastInsertedId);
                }

                pedido.Folio = folioNumero?.ToString() ?? string.Empty;
                pedido.FolioFormateado = folioFormateado;

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo guardar el pedido: {ex.Message}";
                return false;
            }
        }

        private FolioInfo ObtenerFolioInfo(MySqlConnection connection, MySqlTransaction transaction, int empresaId, int? eventoId)
        {
            if (eventoId.HasValue)
            {
                using (var command = new MySqlCommand(@"SELECT tiene_serie, serie, siguiente_folio FROM banquetes.eventos WHERE evento_id = @eventoId FOR UPDATE;", connection, transaction))
                {
                    command.Parameters.AddWithValue("@eventoId", eventoId.Value);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new InvalidOperationException("No se encontró el evento seleccionado.");
                        }

                        var tieneSerie = reader.GetBoolean("tiene_serie");
                        var serie = reader.IsDBNull(reader.GetOrdinal("serie")) ? string.Empty : reader.GetString("serie");
                        var siguienteFolio = reader.GetInt32("siguiente_folio");
                        reader.Close();

                        var folioInfo = new FolioInfo(tieneSerie ? serie : string.Empty, siguienteFolio);

                        using (var update = new MySqlCommand("UPDATE banquetes.eventos SET siguiente_folio = siguiente_folio + 1 WHERE evento_id = @eventoId;", connection, transaction))
                        {
                            update.Parameters.AddWithValue("@eventoId", eventoId.Value);
                            update.ExecuteNonQuery();
                        }

                        return folioInfo;
                    }
                }
            }

            using (var command = new MySqlCommand(@"SELECT sig_folio_pedido FROM banquetes.folios_documentos WHERE empresa_id = @empresaId FOR UPDATE;", connection, transaction))
            {
                command.Parameters.AddWithValue("@empresaId", empresaId);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No se encontró la configuración de folios para la empresa seleccionada.");
                    }

                    var siguienteFolio = reader.GetInt32("sig_folio_pedido");
                    reader.Close();

                    using (var update = new MySqlCommand("UPDATE banquetes.folios_documentos SET sig_folio_pedido = sig_folio_pedido + 1 WHERE empresa_id = @empresaId;", connection, transaction))
                    {
                        update.Parameters.AddWithValue("@empresaId", empresaId);
                        update.ExecuteNonQuery();
                    }

                    return new FolioInfo(string.Empty, siguienteFolio);
                }
            }
        }

        private string ObtenerSerieEvento(MySqlConnection connection, MySqlTransaction transaction, int? eventoId)
        {
            if (!eventoId.HasValue)
            {
                return string.Empty;
            }

            using (var command = new MySqlCommand("SELECT tiene_serie, serie FROM banquetes.eventos WHERE evento_id = @eventoId;", connection, transaction))
            {
                command.Parameters.AddWithValue("@eventoId", eventoId.Value);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return string.Empty;
                    }

                    var tieneSerie = reader.GetBoolean("tiene_serie");
                    if (!tieneSerie)
                    {
                        return string.Empty;
                    }

                    return reader.IsDBNull(reader.GetOrdinal("serie")) ? string.Empty : reader.GetString("serie");
                }
            }
        }

        private static string FormatearFolio(string serie, int consecutivo)
        {
            var numero = consecutivo.ToString("D5");
            return string.IsNullOrEmpty(serie) ? numero : string.Concat(serie, numero);
        }

        private sealed class FolioInfo
        {
            public FolioInfo(string serie, int numero)
            {
                Serie = serie ?? string.Empty;
                Numero = numero;
            }

            public string Serie { get; }

            public int Numero { get; }

            public string Formateado => FormatearFolio(Serie, Numero);
        }
    }
}

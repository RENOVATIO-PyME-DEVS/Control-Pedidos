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

        public bool ActualizarEstatus(int pedidoId, string nuevoEstatus, out string message)
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

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
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
                        }

                        using (var command = new MySqlCommand("UPDATE banquetes.pedidos SET estatus = @estatus WHERE pedido_id = @pedidoId;", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@estatus", nuevoEstatus);
                            command.Parameters.AddWithValue("@pedidoId", pedidoId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
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
                var folioInfo = ObtenerFolioInfo(connection, transaction, empresaId, pedido.Evento?.Id > 0 ? pedido.Evento.Id : (int?)null);
                pedido.Folio = folioInfo.Numero.ToString();
                pedido.FolioFormateado = folioInfo.Formateado;
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
                    command.Parameters.AddWithValue("@folio", folioInfo.Numero);
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

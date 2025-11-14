using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// DAO especializado en recuperar información para los módulos de CheckIN y CheckOUT.
    /// </summary>
    public class PedidoCheckDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public PedidoCheckDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Obtiene el catálogo de eventos disponibles para la empresa indicada.
        /// </summary>
        public IList<Evento> ObtenerEventosPorEmpresa(int empresaId)
        {
            const string query = @"SELECT evento_id,
                                           empresa_id,
                                           nombre,
                                           fecha_evento,
                                           tiene_serie,
                                           serie,
                                           siguiente_folio
                                      FROM banquetes.eventos
                                     WHERE empresa_id = @empresaId
                                     ORDER BY fecha_evento DESC, nombre;";

            var eventos = new List<Evento>();

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@empresaId", empresaId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventos.Add(new Evento
                            {
                                Id = reader.GetInt32("evento_id"),
                                EmpresaId = reader.GetInt32("empresa_id"),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                                FechaEvento = reader.GetDateTime("fecha_evento"),
                                TieneSerie = reader.GetBoolean("tiene_serie"),
                                Serie = reader.IsDBNull(reader.GetOrdinal("serie")) ? string.Empty : reader.GetString("serie"),
                                SiguienteFolio = reader.GetInt32("siguiente_folio")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron recuperar los eventos disponibles", ex);
            }

            return eventos;
        }

        /// <summary>
        /// Recupera los pedidos filtrados por estatus y evento para mostrarlos en los listados principales.
        /// </summary>
        public IList<PedidoCheckInfo> ObtenerPedidosPorEstatus(
            int empresaId,
            string estatus,
            int? eventoId,
            bool filtrarSinEvento,
            string filtroBusqueda,
            bool incluirProductos,
            bool ordenarPorFechaCheckIn)
        {
            if (string.IsNullOrWhiteSpace(estatus))
            {
                throw new ArgumentException("El estatus es obligatorio", nameof(estatus));
            }

            var pedidos = new List<PedidoCheckInfo>();
            var query = BuildBaseSelect(incluirProductos);
            query += " WHERE p.empresa_id = @empresaId AND p.estatus = @estatus";

            if (filtrarSinEvento)
            {
                query += " AND p.evento_id IS NULL";
            }
            else if (eventoId.HasValue)
            {
                query += " AND p.evento_id = @eventoId";
            }

            if (!string.IsNullOrWhiteSpace(filtroBusqueda))
            {
                query += @" AND (
                                UPPER(c.nombre) LIKE CONCAT('%', UPPER(@filtro), '%')
                                OR UPPER(COALESCE(CAST(f_folio_pedido(p.pedido_id) AS CHAR), LPAD(IFNULL(p.folio, p.pedido_id), 5, '0'))) LIKE CONCAT('%', UPPER(@filtro), '%')
                                OR IFNULL(CAST(p.folio AS CHAR), '') LIKE CONCAT('%', @filtro, '%')
                                OR CAST(p.pedido_id AS CHAR) LIKE CONCAT('%', @filtro, '%')
                             )";
            }

            query += ordenarPorFechaCheckIn
                ? " ORDER BY p.fecha_checkin ASC, c.nombre ASC"
                : " ORDER BY p.fecha_entrega ASC, c.nombre ASC";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@empresaId", empresaId);
                    command.Parameters.AddWithValue("@estatus", estatus);

                    if (eventoId.HasValue)
                    {
                        command.Parameters.AddWithValue("@eventoId", eventoId.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(filtroBusqueda))
                    {
                        command.Parameters.AddWithValue("@filtro", filtroBusqueda.Trim());
                    }

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedidos.Add(MapPedido(reader, incluirProductos));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron recuperar los pedidos solicitados", ex);
            }

            return pedidos;
        }

        /// <summary>
        /// Busca un pedido a partir del código escaneado (folio numérico o formateado).
        /// </summary>
        public PedidoCheckInfo ObtenerPedidoPorCodigo(int empresaId, string codigo, string estatusEsperado)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            var consulta = BuildBaseSelect(false) +
                " WHERE p.empresa_id = @empresaId";

            if (!string.IsNullOrWhiteSpace(estatusEsperado))
            {
                consulta += " AND p.estatus = @estatus";
            }

            consulta += @" AND (
                                (@folioNumero IS NOT NULL AND p.folio = @folioNumero)
                                OR (@folioNumero IS NOT NULL AND p.pedido_id = @folioNumero)
                                OR UPPER(COALESCE(CAST(f_folio_pedido(p.pedido_id) AS CHAR), LPAD(IFNULL(p.folio, p.pedido_id), 5, '0'))) = @folioFormateado
                             )
                             LIMIT 1;";

            var codigoNormalizado = codigo.Trim().ToUpperInvariant();
            int parsedFolio;
            int? folioNumero = null;
            if (int.TryParse(RemoveNonNumeric(codigoNormalizado), out parsedFolio))
            {
                folioNumero = parsedFolio;
            }

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(consulta, connection))
                {
                    command.Parameters.AddWithValue("@empresaId", empresaId);
                    if (!string.IsNullOrWhiteSpace(estatusEsperado))
                    {
                        command.Parameters.AddWithValue("@estatus", estatusEsperado);
                    }

                    var folioParameter = new MySqlParameter("@folioNumero", MySqlDbType.Int32)
                    {
                        Value = folioNumero.HasValue ? (object)folioNumero.Value : DBNull.Value
                    };
                    command.Parameters.Add(folioParameter);
                    command.Parameters.AddWithValue("@folioFormateado", codigoNormalizado);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPedido(reader, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No fue posible buscar el pedido por el código proporcionado", ex);
            }

            return null;
        }

        /// <summary>
        /// Valida todas las reglas de negocio necesarias para registrar un CheckIN.
        /// </summary>
        public bool ValidarPedidoParaCheckIn(int empresaId, int pedidoId, int? eventoSeleccionadoId, bool sinEventoSeleccionado, out string mensaje, out PedidoCheckInfo pedido)
        {
            pedido = ObtenerPedidoPorId(empresaId, pedidoId, includeProducts: false);
            if (pedido == null)
            {
                mensaje = "No se encontró la información del pedido.";
                return false;
            }

            if (!string.Equals(pedido.Estatus, "N", StringComparison.OrdinalIgnoreCase))
            {
                mensaje = "El pedido ya fue escaneado o entregado.";
                return false;
            }

            if (pedido.FechaEntrega.Date != DateTime.Today)
            {
                mensaje = "La fecha de entrega del pedido no corresponde al día de hoy.";
                return false;
            }

            if (!pedido.EstaPagado)
            {
                mensaje = "El pedido aún tiene saldo pendiente.";
                return false;
            }

            if (eventoSeleccionadoId.HasValue && pedido.EventoId != eventoSeleccionadoId.Value)
            {
                mensaje = "El pedido pertenece a otro evento.";
                return false;
            }

            if (sinEventoSeleccionado && pedido.EventoId.HasValue)
            {
                mensaje = "El pedido tiene un evento asignado y no puede registrarse como 'Sin evento'.";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }

        /// <summary>
        /// Ejecuta la actualización de base de datos para pasar el pedido a estatus CI.
        /// </summary>
        public bool RegistrarCheckIn(int pedidoId, out string mensaje)
        {
            const string update = @"UPDATE banquetes.pedidos
                                      SET estatus = 'CI',
                                          fecha_checkin = CONVERT_TZ(NOW(), '+00:00', '-06:00')
                                    WHERE pedido_id = @pedidoId;";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(update, connection))
                {
                    command.Parameters.AddWithValue("@pedidoId", pedidoId);

                    connection.Open();
                    var affected = command.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        mensaje = "No se actualizó ningún registro.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"No se pudo registrar el CheckIN: {ex.Message}";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }

        /// <summary>
        /// Valida que el pedido se encuentre en estatus CI para poder liberarlo.
        /// </summary>
        public bool ValidarPedidoParaCheckOut(int empresaId, int pedidoId, out string mensaje, out PedidoCheckInfo pedido)
        {
            pedido = ObtenerPedidoPorId(empresaId, pedidoId, includeProducts: true);
            if (pedido == null)
            {
                mensaje = "No se encontró la información del pedido.";
                return false;
            }

            if (!string.Equals(pedido.Estatus, "CI", StringComparison.OrdinalIgnoreCase))
            {
                mensaje = "El pedido aún no tiene CheckIN.";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }

        /// <summary>
        /// Actualiza el estatus del pedido a CO para indicar que fue entregado al cliente.
        /// </summary>
        public bool RegistrarCheckOut(int pedidoId, out string mensaje)
        {
            const string update = @"UPDATE banquetes.pedidos
                                      SET estatus = 'CO'
                                    WHERE pedido_id = @pedidoId;";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(update, connection))
                {
                    command.Parameters.AddWithValue("@pedidoId", pedidoId);

                    connection.Open();
                    var affected = command.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        mensaje = "No se actualizó ningún registro.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"No se pudo registrar el CheckOUT: {ex.Message}";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }

        /// <summary>
        /// Recupera un pedido específico incluyendo datos financieros para las validaciones.
        /// </summary>
        private PedidoCheckInfo ObtenerPedidoPorId(int empresaId, int pedidoId, bool includeProducts)
        {
            var query = BuildBaseSelect(includeProducts) + " WHERE p.empresa_id = @empresaId AND p.pedido_id = @pedidoId LIMIT 1;";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@empresaId", empresaId);
                    command.Parameters.AddWithValue("@pedidoId", pedidoId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPedido(reader, includeProducts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudo recuperar el pedido especificado", ex);
            }

            return null;
        }

        /// <summary>
        /// Construye el SELECT común utilizado en las consultas de los módulos.
        /// </summary>
        private static string BuildBaseSelect(bool includeProducts)
        {
            var select = @"SELECT
                                p.pedido_id,
                                p.folio,
                                COALESCE(CAST(f_folio_pedido(p.pedido_id) AS CHAR), LPAD(IFNULL(p.folio, p.pedido_id), 5, '0')) AS folio_formateado,
                                c.nombre AS cliente_nombre,
                                p.fecha_entrega,
                                p.hora_entrega,
                                p.estatus,
                                p.evento_id,
                                IFNULL(e.nombre, '') AS evento_nombre,
                                p.fecha_checkin,
                                IFNULL(f_totalPedido(p.pedido_id), 0) - IFNULL(p.descuento, 0) AS total,
                                IFNULL(f_cobroPedido(p.pedido_id), 0) AS abonado,
                                (IFNULL(f_totalPedido(p.pedido_id), 0) - IFNULL(p.descuento, 0)) - IFNULL(f_cobroPedido(p.pedido_id), 0) AS saldo_pendiente";

            if (includeProducts)
            {
                select += ", IFNULL(prod.descripcion, '') AS productos_descripcion";
            }

            select += @" FROM banquetes.pedidos p
                           INNER JOIN banquetes.clientes c ON c.cliente_id = p.cliente_id
                           LEFT JOIN banquetes.eventos e ON e.evento_id = p.evento_id";

            if (includeProducts)
            {
                select += @" LEFT JOIN (
                                    SELECT pd.pedido_id,
                                           GROUP_CONCAT(CONCAT(pd.cantidad, ' x ', COALESCE(a.nombre, 'Artículo')) ORDER BY a.nombre SEPARATOR '\n') AS descripcion
                                      FROM banquetes.pedidos_detalles pd
                                      LEFT JOIN banquetes.articulos a ON a.articulo_id = pd.articulo_id
                                     GROUP BY pd.pedido_id
                                ) prod ON prod.pedido_id = p.pedido_id";
            }

            return select;
        }

        /// <summary>
        /// Convierte el resultado de la consulta en una instancia del modelo de Check.
        /// </summary>
        private static PedidoCheckInfo MapPedido(MySqlDataReader reader, bool includeProducts)
        {
            var pedido = new PedidoCheckInfo
            {
                PedidoId = reader.GetInt32("pedido_id"),
                FolioFormateado = reader.GetString("folio_formateado"),
                ClienteNombre = reader.GetString("cliente_nombre"),
                FechaEntrega = reader.GetDateTime("fecha_entrega"),
                Estatus = reader.GetString("estatus"),
                EventoNombre = reader.IsDBNull(reader.GetOrdinal("evento_nombre")) ? string.Empty : reader.GetString("evento_nombre"),
                Total = reader.GetDecimal("total"),
                Abonado = reader.GetDecimal("abonado"),
                SaldoPendiente = reader.GetDecimal("saldo_pendiente"),
                FechaCheckIn = reader.IsDBNull(reader.GetOrdinal("fecha_checkin")) ? (DateTime?)null : reader.GetDateTime("fecha_checkin"),
                EventoId = reader.IsDBNull(reader.GetOrdinal("evento_id")) ? (int?)null : reader.GetInt32("evento_id"),
                FolioNumerico = reader.IsDBNull(reader.GetOrdinal("folio")) ? (int?)null : reader.GetInt32("folio")
            };

            var horaOrdinal = reader.GetOrdinal("hora_entrega");
            pedido.HoraEntrega = reader.IsDBNull(horaOrdinal) ? (TimeSpan?)null : reader.GetTimeSpan("hora_entrega");

            if (includeProducts)
            {
                pedido.ProductosDescripcion = reader.IsDBNull(reader.GetOrdinal("productos_descripcion"))
                    ? string.Empty
                    : reader.GetString("productos_descripcion");
            }

            return pedido;
        }

        /// <summary>
        /// Elimina todos los caracteres no numéricos de un código para poder obtener el folio.
        /// </summary>
        private static string RemoveNonNumeric(string value)
        {
            var result = new StringBuilder();
            foreach (var ch in value)
            {
                if (char.IsDigit(ch))
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }
    }
}

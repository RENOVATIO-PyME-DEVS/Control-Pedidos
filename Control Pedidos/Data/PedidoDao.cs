using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                            ? "UPDATE banquetes.pedidos SET estatus = @estatus, folio = @folio, impreso = CASE WHEN @estatus = 'N' THEN 'N' ELSE impreso END WHERE pedido_id = @pedidoId;"
                            : "UPDATE banquetes.pedidos SET estatus = @estatus, impreso = CASE WHEN @estatus = 'N' THEN 'N' ELSE impreso END WHERE pedido_id = @pedidoId;";

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

        public Pedido ObtenerPedidoCompleto(int pedidoId)
        {
            if (pedidoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pedidoId));
            }

            using (var connection = _connectionFactory.Create())
            {
                connection.Open();

                Pedido pedido = null;
                string serieEvento = string.Empty;
                bool eventoTieneSerie = false;

                using (var pedidoCommand = new MySqlCommand(@"SELECT
                                p.pedido_id,
                                p.folio,
                                p.fecha,
                                p.fecha_entrega,
                                p.hora_entrega,
                                p.requiere_factura,
                                p.notas,
                                p.fecha_creacion,
                                p.estatus,
                                p.descuento,
                                p.usuario_descuento,
                                p.impreso,
                                p.evento_id,
                                c.cliente_id,
                                c.nombre AS cliente_nombre,
                                c.telefono AS cliente_telefono,
                                c.correo AS cliente_correo,
                                c.domicilio AS cliente_domicilio,
                                e.empresa_id,
                                e.nombre AS empresa_nombre,
                                e.rfc AS empresa_rfc,
                                e.domicilio AS empresa_domicilio,
                                e.telefono AS empresa_telefono,
                                e.correo AS empresa_correo,
                                u.usuario_id,
                                u.nombre AS usuario_nombre,
                                u.correo AS usuario_correo,
                                ev.nombre AS evento_nombre,
                                ev.fecha_evento,
                                ev.tiene_serie,
                                ev.serie
                            FROM banquetes.pedidos p
                            INNER JOIN banquetes.clientes c ON c.cliente_id = p.cliente_id
                            INNER JOIN banquetes.empresas e ON e.empresa_id = p.empresa_id
                            INNER JOIN banquetes.usuarios u ON u.usuario_id = p.usuario_id
                            LEFT JOIN banquetes.eventos ev ON ev.evento_id = p.evento_id
                            WHERE p.pedido_id = @pedidoId;", connection))
                {
                    pedidoCommand.Parameters.AddWithValue("@pedidoId", pedidoId);

                    using (var reader = pedidoCommand.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        var folioNumero = reader.IsDBNull(reader.GetOrdinal("folio")) ? (int?)null : reader.GetInt32("folio");
                        var requiereFactura = !reader.IsDBNull(reader.GetOrdinal("requiere_factura")) && string.Equals(reader.GetString("requiere_factura"), "S", StringComparison.OrdinalIgnoreCase);
                        var horaEntrega = reader.IsDBNull(reader.GetOrdinal("hora_entrega")) ? (TimeSpan?)null : reader.GetTimeSpan(reader.GetOrdinal("hora_entrega"));
                        var notas = reader.IsDBNull(reader.GetOrdinal("notas")) ? string.Empty : reader.GetString("notas");
                        var descuento = reader.IsDBNull(reader.GetOrdinal("descuento")) ? 0m : reader.GetDecimal("descuento");
                        var usuarioDescuento = reader.IsDBNull(reader.GetOrdinal("usuario_descuento")) ? string.Empty : reader.GetString("usuario_descuento");
                        var impreso = reader.IsDBNull(reader.GetOrdinal("impreso")) ? "N" : reader.GetString("impreso");

                        var cliente = new Cliente
                        {
                            Id = reader.GetInt32("cliente_id"),
                            Nombre = reader.GetString("cliente_nombre"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("cliente_telefono")) ? string.Empty : reader.GetString("cliente_telefono"),
                            Correo = reader.IsDBNull(reader.GetOrdinal("cliente_correo")) ? string.Empty : reader.GetString("cliente_correo"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("cliente_domicilio")) ? string.Empty : reader.GetString("cliente_domicilio")
                        };

                        var empresa = new Empresa
                        {
                            Id = reader.GetInt32("empresa_id"),
                            Nombre = reader.GetString("empresa_nombre"),
                            Rfc = reader.IsDBNull(reader.GetOrdinal("empresa_rfc")) ? string.Empty : reader.GetString("empresa_rfc"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("empresa_domicilio")) ? string.Empty : reader.GetString("empresa_domicilio"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("empresa_telefono")) ? string.Empty : reader.GetString("empresa_telefono"),
                            Correo = reader.IsDBNull(reader.GetOrdinal("empresa_correo")) ? string.Empty : reader.GetString("empresa_correo")
                        };

                        var usuario = new Usuario
                        {
                            Id = reader.GetInt32("usuario_id"),
                            Nombre = reader.GetString("usuario_nombre"),
                            Correo = reader.IsDBNull(reader.GetOrdinal("usuario_correo")) ? string.Empty : reader.GetString("usuario_correo")
                        };

                        Evento evento = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("evento_id")))
                        {
                            var eventoId = reader.GetInt32("evento_id");
                            eventoTieneSerie = !reader.IsDBNull(reader.GetOrdinal("tiene_serie")) && reader.GetBoolean("tiene_serie");
                            serieEvento = eventoTieneSerie && !reader.IsDBNull(reader.GetOrdinal("serie")) ? reader.GetString("serie") : string.Empty;

                            evento = new Evento
                            {
                                Id = eventoId,
                                EmpresaId = empresa.Id,
                                Nombre = reader.IsDBNull(reader.GetOrdinal("evento_nombre")) ? string.Empty : reader.GetString("evento_nombre"),
                                FechaEvento = reader.IsDBNull(reader.GetOrdinal("fecha_evento")) ? DateTime.MinValue : reader.GetDateTime("fecha_evento"),
                                TieneSerie = eventoTieneSerie,
                                Serie = serieEvento
                            };
                        }

                        pedido = new Pedido
                        {
                            Id = pedidoId,
                            Folio = folioNumero?.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
                            Cliente = cliente,
                            Empresa = empresa,
                            Usuario = usuario,
                            Evento = evento,
                            Fecha = reader.GetDateTime("fecha"),
                            FechaEntrega = reader.GetDateTime("fecha_entrega"),
                            HoraEntrega = horaEntrega,
                            RequiereFactura = requiereFactura,
                            Notas = notas,
                            FechaCreacion = reader.GetDateTime("fecha_creacion"),
                            Estatus = reader.GetString("estatus"),
                            Descuento = descuento,
                            UsuarioDescuento = usuarioDescuento,
                            Impreso = impreso
                        };

                        if (folioNumero.HasValue)
                        {
                            var serie = eventoTieneSerie ? serieEvento : string.Empty;
                            pedido.FolioFormateado = FormatearFolio(serie, folioNumero.Value);
                        }
                    }
                }

                if (pedido == null)
                {
                    return null;
                }

                var detalles = new List<PedidoDetalle>();
                using (var detalleCommand = new MySqlCommand(@"SELECT
                                d.pedido_detalle_id,
                                d.articulo_id,
                                d.cantidad,
                                d.precio_unitario,
                                d.total,
                                a.nombre AS articulo_nombre,
                                a.nombre_corto,
                                a.tipo_articulo,
                                a.unidad_medida
                            FROM banquetes.pedidos_detalles d
                            INNER JOIN banquetes.articulos a ON a.articulo_id = d.articulo_id
                            WHERE d.pedido_id = @pedidoId
                            ORDER BY d.pedido_detalle_id;", connection))
                {
                    detalleCommand.Parameters.AddWithValue("@pedidoId", pedidoId);

                    using (var reader = detalleCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var articulo = new Articulo
                            {
                                Id = reader.GetInt32("articulo_id"),
                                Nombre = reader.GetString("articulo_nombre"),
                                NombreCorto = reader.IsDBNull(reader.GetOrdinal("nombre_corto")) ? string.Empty : reader.GetString("nombre_corto"),
                                TipoArticulo = reader.IsDBNull(reader.GetOrdinal("tipo_articulo")) ? string.Empty : reader.GetString("tipo_articulo"),
                            UnidadMedida = reader.GetString("unidad_medida")
                            };

                            var detalle = new PedidoDetalle
                            {
                                Id = reader.GetInt32("pedido_detalle_id"),
                                PedidoId = pedidoId,
                                ArticuloId = articulo.Id,
                                Articulo = articulo,
                                Cantidad = reader.GetDecimal("cantidad"),
                                PrecioUnitario = reader.GetDecimal("precio_unitario"),
                                Total = reader.GetDecimal("total")
                            };

                            detalles.Add(detalle);
                        }
                    }
                }

                if (detalles.Count > 0)
                {
                    var kitArticuloIds = detalles
                        .Where(d => d.Articulo != null && d.Articulo.EsKit)
                        .Select(d => d.Articulo.Id)
                        .Distinct()
                        .ToList();

                    if (kitArticuloIds.Count > 0)
                    {
                        var parameterNames = kitArticuloIds.Select((_, index) => $"@kitId{index}").ToList();
                        var query = $@"SELECT kd.articulo_id AS kit_id, kd.articulo_compuesto_id, kd.visible, kd.cantidad, a.nombre, a.nombre_corto, a.unidad_medida
                                FROM banquetes.articulos_kit kd
                                INNER JOIN banquetes.articulos a ON a.articulo_id = kd.articulo_compuesto_id
                                WHERE kd.articulo_id IN ({string.Join(",", parameterNames)});";

                        using (var componentesCommand = new MySqlCommand(query, connection))
                        {
                            for (var i = 0; i < kitArticuloIds.Count; i++)
                            {
                                componentesCommand.Parameters.AddWithValue(parameterNames[i], kitArticuloIds[i]);
                            }

                            using (var reader = componentesCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var kitId = reader.GetInt32("kit_id");
                                    var detalle = detalles.FirstOrDefault(d => d.Articulo != null && d.Articulo.Id == kitId);
                                    if (detalle == null)
                                    {
                                        continue;
                                    }

                                    detalle.Componentes.Add(new KitDetalle
                                    {
                                        KitId = kitId,
                                        ArticuloId = reader.GetInt32("articulo_compuesto_id"),
                                        Cantidad = reader.GetDecimal("cantidad"),
                                        Visible = reader.GetString("visible"),
                                        Articulo = new Articulo
                                        {
                                            Id = reader.GetInt32("articulo_compuesto_id"),
                                            Nombre = reader.GetString("nombre"),
                                            NombreCorto = reader.IsDBNull(reader.GetOrdinal("nombre_corto")) ? string.Empty : reader.GetString("nombre_corto"),
                                            UnidadMedida = reader.GetString("unidad_medida")
                                        }
                                    });
                                }
                            }
                        }
                    }
                }

                pedido.Detalles = detalles;
                pedido.Subtotal = detalles.Sum(d => d.Total);
                pedido.Total = Math.Max(0m, pedido.Subtotal - pedido.Descuento);

                // Se calcula el total de cobros registrados para el pedido.
                decimal totalAbonado = 0m;
                using (var cobrosCommand = new MySqlCommand(@"SELECT IFNULL(SUM(det.monto), 0)
FROM banquetes.cobros_pedidos_det det
INNER JOIN banquetes.cobros_pedidos cp ON cp.cobro_pedido = det.cobro_pedido_id
WHERE det.pedido_id = @pedidoId;", connection))
                {
                    cobrosCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                    var resultado = cobrosCommand.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        totalAbonado = Convert.ToDecimal(resultado);
                    }
                }

                // Se obtiene la forma de pago del último cobro aplicado al pedido.
                string formaCobro = string.Empty;
                using (var formaCommand = new MySqlCommand(@"SELECT IFNULL(fc.nombre, '') AS forma
FROM banquetes.cobros_pedidos cp
INNER JOIN banquetes.cobros_pedidos_det det ON det.cobro_pedido_id = cp.cobro_pedido
LEFT JOIN banquetes.formas_cobros fc ON fc.forma_cobro_id = cp.forma_cobro_id
WHERE det.pedido_id = @pedidoId
ORDER BY cp.fecha DESC, cp.cobro_pedido DESC
LIMIT 1;", connection))
                {
                    formaCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                    var resultado = formaCommand.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        formaCobro = Convert.ToString(resultado);
                    }
                }

                pedido.MontoAbonado = totalAbonado;
                pedido.FormaCobroUltima = formaCobro;
                pedido.SaldoPendiente = Math.Max(0m, pedido.Total - totalAbonado);

                return pedido;
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
                            ? "UPDATE banquetes.pedidos SET estatus = @estatus, folio = @folio, descuento = @descuento, usuario_descuento = @usuarioDescuento, impreso = 'N' WHERE pedido_id = @pedidoId;"
                            : "UPDATE banquetes.pedidos SET estatus = @estatus, descuento = @descuento, usuario_descuento = @usuarioDescuento, impreso = 'N' WHERE pedido_id = @pedidoId;";

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

        public void ActualizarImpresion(int pedidoId, bool impreso)
        {
            if (pedidoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pedidoId));
            }

            var valor = impreso ? "S" : "N";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand("UPDATE banquetes.pedidos SET impreso = @impreso WHERE pedido_id = @pedidoId;", connection))
            {
                command.Parameters.AddWithValue("@impreso", valor);
                command.Parameters.AddWithValue("@pedidoId", pedidoId);

                connection.Open();
                command.ExecuteNonQuery();
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

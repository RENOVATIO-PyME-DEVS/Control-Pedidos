using Control_Pedidos.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Control_Pedidos.Data
{
    /*
     * Clase: DevolucionDao
     * Descripción: Expone las consultas y operaciones necesarias para obtener pedidos activos
     *              y registrar su devolución automática. Cada método está ampliamente documentado
     *              para detallar los pasos que ejecuta contra la base de datos.
     */
    public class DevolucionDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        /*
         * Constructor: DevolucionDao
         * Descripción: Recibe la fábrica de conexiones utilizada para abrir conexiones MySQL
         *              cada vez que se requiere consultar o modificar la información de pedidos.
         */
        public DevolucionDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /*
         * Método: ObtenerPedidosActivos
         * Descripción: Recupera todos los pedidos con estatus 'N' del cliente. El resultado incluye
         *              el folio visible, la fecha de creación, el total calculado mediante la función
         *              f_totalPedido, el monto abonado (f_cobroPedido) y el saldo restante.
         */
        public List<Pedido> ObtenerPedidosActivos(int clienteId)
        {
            var pedidos = new List<Pedido>();

            // Consulta basada en las funciones auxiliares proporcionadas para obtener totales y cobros.
            const string query = @"SELECT
    p.pedido_id,
    COALESCE(CAST(f_folio_pedido(p.pedido_id) AS CHAR), CAST(p.pedido_id AS CHAR)) AS folio,
    p.fecha,
    IFNULL(f_totalPedido(p.pedido_id), 0) AS total,
    IFNULL(f_cobroPedido(p.pedido_id), 0) AS abonado,
    p.estatus
FROM banquetes.pedidos p
WHERE p.cliente_id = @clienteId
  AND p.estatus = 'N'
ORDER BY p.fecha DESC, p.pedido_id DESC;";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                // Se parametriza el identificador del cliente para evitar inyecciones SQL.
                command.Parameters.AddWithValue("@clienteId", clienteId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Por cada registro se crea la entidad Pedido con los valores calculados.
                        var fechaOrdinal = reader.GetOrdinal("fecha");
                        var pedido = new Pedido
                        {
                            Id = reader.GetInt32("pedido_id"),
                            Folio = reader.IsDBNull(reader.GetOrdinal("folio")) ? string.Empty : reader.GetString("folio"),
                            Fecha = reader.IsDBNull(fechaOrdinal) ? DateTime.Now : reader.GetDateTime(fechaOrdinal),
                            Total = reader.GetDecimal("total"),
                            MontoAbonado = reader.GetDecimal("abonado"),
                            Estatus = reader.GetString("estatus")
                        };

                        // El saldo pendiente se calcula restando el abonado al total.
                        pedido.SaldoPendiente = pedido.Total - pedido.MontoAbonado;
                        pedidos.Add(pedido);
                    }
                }
            }

            return pedidos;
        }

        /*
         * Método: ObtenerTotalCobros
         * Descripción: Llama a la función f_cobroPedido para conocer el monto acumulado
         *              de cobros asociados al pedido. Se utiliza para determinar si la
         *              devolución requiere generar un movimiento monetario.
         */
        public decimal ObtenerTotalCobros(int pedidoId)
        {
            const string query = "SELECT IFNULL(f_cobroPedido(@pedidoId), 0);";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@pedidoId", pedidoId);
                connection.Open();

                var result = command.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
            }
        }

        /*
         * Método: ObtenerFormaCobroUltimoAbono
         * Descripción: Consulta el último cobro aplicado al pedido para mostrar la forma de pago
         *              utilizada. Si nunca se ha registrado un abono, devuelve una cadena vacía.
         */
        public string ObtenerFormaCobroUltimoAbono(int pedidoId)
        {
            const string query = @"SELECT fc.nombre
FROM banquetes.cobros_pedidos cp
INNER JOIN banquetes.cobros_pedidos_det det ON det.cobro_pedido_id = cp.cobro_pedido
LEFT JOIN banquetes.formas_cobros fc ON fc.forma_cobro_id = cp.forma_cobro_id
WHERE det.pedido_id = @pedidoId
ORDER BY cp.fecha_creacion DESC
LIMIT 1;";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@pedidoId", pedidoId);
                connection.Open();

                var result = command.ExecuteScalar();
                return result == null || result == DBNull.Value ? string.Empty : Convert.ToString(result);
            }
        }

        /*
         * Método: ObtenerFormasCobroDevolucion
         * Descripción: Devuelve el catálogo de formas de cobro marcadas como tipo 'D',
         *              que representan instrumentos válidos para devolver dinero al cliente.
         */
        public List<FormaCobro> ObtenerFormasCobroDevolucion()
        {
            var formas = new List<FormaCobro>();
            const string query = @"SELECT forma_cobro_id, nombre, IFNULL(tipo_cobro, '') AS descripcion, tipo
FROM banquetes.formas_cobros
WHERE tipo = 'D'
ORDER BY nombre;";

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

        /*
         * Método: RegistrarDevolucion
         * Descripción: Genera la devolución del pedido indicado. Si el pedido tiene cobros registrados,
         *              se crea un movimiento en cobros_pedidos de tipo devolución y se liga con el pedido.
         *              Finalmente el pedido se marca con estatus 'D'.
         */
        public bool RegistrarDevolucion(int pedidoId, int usuarioId, int formaCobroId, out string message)
        {
            message = string.Empty;

            // Se reinicia el resultado previo para que la interfaz pueda conocer el último cobro generado.
            UltimoCobroGenerado = null;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Bloquea el pedido para evitar que otro proceso lo modifique mientras se registra la devolución.
                        const string pedidoInfoQuery = @"SELECT cliente_id, empresa_id, estatus
FROM banquetes.pedidos
WHERE pedido_id = @pedidoId
FOR UPDATE;";

                        int clienteId;
                        int empresaId;
                        string estatusActual;

                        using (var pedidoCommand = new MySqlCommand(pedidoInfoQuery, connection, transaction))
                        {
                            pedidoCommand.Parameters.AddWithValue("@pedidoId", pedidoId);

                            using (var reader = pedidoCommand.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    transaction.Rollback();
                                    message = "No se encontró el pedido solicitado.";
                                    return false;
                                }

                                clienteId = reader.GetInt32("cliente_id");
                                empresaId = reader.GetInt32("empresa_id");
                                estatusActual = reader.GetString("estatus");
                            }
                        }

                        // Verifica que el pedido continúe activo antes de intentar cancelarlo.
                        if (!string.Equals(estatusActual, "N", StringComparison.OrdinalIgnoreCase))
                        {
                            transaction.Rollback();
                            message = "El pedido ya no se encuentra activo.";
                            return false;
                        }

                        // Recupera los montos calculados directamente desde la base de datos.
                        decimal totalPedido;
                        decimal totalCobros;

                        using (var totalCommand = new MySqlCommand("SELECT IFNULL(f_totalPedido(@pedidoId), 0);", connection, transaction))
                        {
                            totalCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                            var totalResult = totalCommand.ExecuteScalar();
                            totalPedido = totalResult == null || totalResult == DBNull.Value ? 0m : Convert.ToDecimal(totalResult);
                        }

                        using (var cobrosCommand = new MySqlCommand("SELECT IFNULL(f_cobroPedido(@pedidoId), 0);", connection, transaction))
                        {
                            cobrosCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                            var cobrosResult = cobrosCommand.ExecuteScalar();
                            totalCobros = cobrosResult == null || cobrosResult == DBNull.Value ? 0m : Convert.ToDecimal(cobrosResult);
                        }

                        long cobroId = 0;

                        // Cuando existen cobros es obligatorio registrar la devolución monetaria.
                        if (totalCobros > 0)
                        {
                            if (formaCobroId <= 0)
                            {
                                transaction.Rollback();
                                message = "Seleccione la forma de devolución que se aplicará al cliente.";
                                return false;
                            }

                            const string insertCobroSql = @"INSERT INTO banquetes.cobros_pedidos
    (usuario_id, empresa_id, cliente_id, forma_cobro_id, monto, fecha, fecha_creacion, estatus, impreso)
VALUES
    (@usuarioId, @empresaId, @clienteId, @formaCobroId, @monto, NOW(), NOW(), 'N', 'N');";

                            using (var insertCobroCommand = new MySqlCommand(insertCobroSql, connection, transaction))
                            {
                                insertCobroCommand.Parameters.AddWithValue("@usuarioId", usuarioId);
                                insertCobroCommand.Parameters.AddWithValue("@empresaId", empresaId);
                                insertCobroCommand.Parameters.AddWithValue("@clienteId", clienteId);
                                insertCobroCommand.Parameters.AddWithValue("@formaCobroId", formaCobroId);
                                insertCobroCommand.Parameters.AddWithValue("@monto", totalCobros);

                                insertCobroCommand.ExecuteNonQuery();
                                cobroId = insertCobroCommand.LastInsertedId;
                            }

                            const string insertDetalleSql = @"INSERT INTO banquetes.cobros_pedidos_det
    (cobro_pedido_id, pedido_id, monto)
VALUES
    (@cobroId, @pedidoId, @monto);";

                            using (var insertDetalleCommand = new MySqlCommand(insertDetalleSql, connection, transaction))
                            {
                                insertDetalleCommand.Parameters.AddWithValue("@cobroId", cobroId);
                                insertDetalleCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                                insertDetalleCommand.Parameters.AddWithValue("@monto", totalCobros);
                                insertDetalleCommand.ExecuteNonQuery();
                            }

                            // Se conserva la información para que la capa de presentación pueda imprimir el comprobante.
                            UltimoCobroGenerado = new Cobro
                            {
                                Id = (int)cobroId,
                                CobroPedidoId = (int)cobroId,
                                UsuarioId = usuarioId,
                                EmpresaId = empresaId,
                                ClienteId = clienteId,
                                FormaCobroId = formaCobroId,
                                Monto = totalCobros,
                                Fecha = DateTime.Now,
                                FechaCreacion = DateTime.Now,
                                Estatus = "N",
                                SaldoAnterior = totalPedido,
                                SaldoDespues = totalPedido - totalCobros
                            };
                        }

                        // Actualiza el estatus del pedido a devuelto.
                        using (var updatePedidoCommand = new MySqlCommand("UPDATE banquetes.pedidos SET estatus = 'D' WHERE pedido_id = @pedidoId;", connection, transaction))
                        {
                            updatePedidoCommand.Parameters.AddWithValue("@pedidoId", pedidoId);
                            updatePedidoCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        message = "Pedido devuelto correctamente.";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Si ocurre cualquier error se notifica con un mensaje descriptivo.
                message = $"No se pudo registrar la devolución: {ex.Message}";
                UltimoCobroGenerado = null;
                return false;
            }
        }

        /*
         * Propiedad: UltimoCobroGenerado
         * Descripción: Expone el movimiento de devolución creado durante la última ejecución
         *              de RegistrarDevolucion para que la interfaz imprima el ticket correspondiente.
         */
        public Cobro UltimoCobroGenerado { get; private set; }
    }
}

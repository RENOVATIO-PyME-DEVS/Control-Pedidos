using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /*
     * Clase: CorteCajaDao
     * Descripción: Encapsula el acceso al procedimiento almacenado SP_CorteCaja
     *              para recuperar los movimientos diarios de caja. Mantener esta
     *              lógica aislada facilita reutilizarla desde cualquier formulario
     *              o servicio sin duplicar código SQL.
     */
    public class CorteCajaDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        /// <summary>
        /// Inicializa el DAO asegurando que siempre exista una fábrica de conexiones válida.
        /// </summary>
        /// <param name="connectionFactory">Fábrica responsable de construir conexiones MySQL configuradas.</param>
        public CorteCajaDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado de corte de caja y devuelve el resultado en una tabla en memoria.
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa cuyos movimientos se desean consultar.</param>
        /// <param name="usuarioId">Identificador del usuario responsable del corte.</param>
        /// <param name="fecha">Fecha del corte (solo la parte de fecha es considerada por el SP).</param>
        /// <returns>Tabla con los movimientos devueltos por el procedimiento almacenado.</returns>
        public DataTable EjecutarCorte(int empresaId, int usuarioId, DateTime fecha)
        {
            // Se crea un DataTable vacío con un nombre descriptivo para facilitar la depuración.
            var tabla = new DataTable("CorteCaja");

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand("SP_CorteCaja", connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                // Indicamos que vamos a ejecutar un procedimiento almacenado para que el proveedor configure
                // automáticamente la llamada como CALL SP_CorteCaja(@empresa_id, @usuario_id, @fecha).
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros esperados por el SP. Se envía la fecha sin componente de hora para coincidir con la lógica del corte.
                command.Parameters.AddWithValue("@empresa_id", empresaId);
                command.Parameters.AddWithValue("@usuario_id", usuarioId);
                command.Parameters.AddWithValue("@fecha", fecha.Date);

                connection.Open();

                // Llenamos el DataTable con el resultado completo del SP, incluyendo totales o subtotales si existen.
                adapter.Fill(tabla);
            }

            return tabla;
        }
    }
}

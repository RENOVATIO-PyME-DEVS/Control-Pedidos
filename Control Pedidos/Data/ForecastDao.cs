using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// Acceso a datos para los reportes de forecast de producción y ventas.
    /// </summary>
    public class ForecastDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public ForecastDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public DataTable ObtenerForecast(int empresaId, int eventoId)
        {
            var table = new DataTable();

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand("CALL banquetes.reporte_avancepresupuesto(@empresaID, @eventoID);", connection))
                using (var adapter = new MySqlDataAdapter(command))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@empresaID", empresaId);
                    command.Parameters.AddWithValue("@eventoID", eventoId);

                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudo obtener el forecast del evento seleccionado.", ex);
            }

            return table;
        }
    }
}

using System;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    public class DatabaseConnectionFactory
    {
        private readonly DatabaseConfig _config;

        public DatabaseConnectionFactory(DatabaseConfig config)
        {
            // La configuración llega desde afuera y la guardamos para armar conexiones más adelante.
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public MySqlConnection Create()
        {
            // Cada llamada devuelve una conexión nueva basada en la config actual.
            return new MySqlConnection(BuildConnectionString());
        }

        public bool TestConnection(out string message)
        {
            try
            {
                using (var connection = Create())
                {
                    // Abrimos y cerramos enseguida solo para asegurarnos de que los datos son correctos.
                    connection.Open();
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Si falla guardamos el mensaje para mostrarlo en la UI.
                message = ex.Message;
                return false;
            }
        }

        private string BuildConnectionString()
        {
            // Armamos el connection string con SSL y retrieval de llave pública para conexiones más seguras.
            return string.Format(
                "Server={0};Database={1};Uid={2};Pwd={3};SslMode=Required;AllowPublicKeyRetrieval=True",
                _config.Host,
                _config.Database,
                _config.Username,
                _config.Password);
        }
    }
}

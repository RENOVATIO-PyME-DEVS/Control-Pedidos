using System;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    public class DatabaseConnectionFactory
    {
        private readonly DatabaseConfig _config;

        public DatabaseConnectionFactory(DatabaseConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public MySqlConnection Create()
        {
            return new MySqlConnection(BuildConnectionString());
        }

        public bool TestConnection(out string message)
        {
            try
            {
                using (var connection = Create())
                {
                    connection.Open();
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        private string BuildConnectionString()
        {
            return string.Format(
                "Server={0};Database={1};Uid={2};Pwd={3};SslMode=Required;AllowPublicKeyRetrieval=True",
                _config.Host,
                _config.Database,
                _config.Username,
                _config.Password);
        }
    }
}

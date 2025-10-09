using System;
using MySql.Data.MySqlClient;
using Control_Pedidos.Data;

namespace Control_Pedidos.Controllers
{
    public class AuthController
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public AuthController(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool TryLogin(string username, string password, out string role)
        {
            role = string.Empty;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            const string query = @"SELECT role FROM users WHERE username = @username AND password_hash = SHA2(@password, 256)";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al validar las credenciales de usuario", ex);
            }

            return false;
        }
    }
}

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

        public bool TryLogin(string username, string password, out string role, out string nombreuser, out string usuarioid)
        {
            role = string.Empty;
            nombreuser = string.Empty;
            usuarioid = string.Empty;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            //const string query = @"SELECT rol_usuario_id
            //                        FROM usuarios
            //                        WHERE correo = @username
            //                          AND pass = SHA2(@password, 256)
            //                          AND (estatus IS NULL OR estatus <> 'B')";

            const string query = @"SELECT ru.nombre as rol_nombre, u.nombre as usuario_nombre, u.usuario_id as usuario_id
	                                FROM banquetes.usuarios u
                                    LEFT JOIN banquetes.roles_usuarios ru on ru.rol_usuario_id = u.rol_usuario_id
	                                WHERE u.correo = @username
	                                AND u.pass = SHA2(@password, 256)
	                                AND (u.estatus IS NULL OR estatus <> 'B');";
            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //leemos los campos
                            role = reader["rol_nombre"].ToString();
                            nombreuser = reader["usuario_nombre"].ToString();
                            usuarioid = reader["usuario_id"].ToString();
                            return true;
                        }
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

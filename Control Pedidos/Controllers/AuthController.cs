using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Control_Pedidos.Data;
using Control_Pedidos.Models;

namespace Control_Pedidos.Controllers
{
    public class AuthController
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public AuthController(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool TryLogin(string username, string password, out string role, out string nombreuser, out string usuarioid, out List<Empresa> empresas)
        {
            role = string.Empty;
            nombreuser = string.Empty;
            usuarioid = string.Empty;
            empresas = new List<Empresa>();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            const string query = @"SELECT ru.nombre as rol_nombre, u.nombre as usuario_nombre, u.usuario_id as usuario_id
                                        FROM banquetes.usuarios u
                                    LEFT JOIN banquetes.roles_usuarios ru on ru.rol_usuario_id = u.rol_usuario_id
                                        WHERE u.correo = @username
                                        AND u.pass = SHA2(@password, 256)
                                        AND (u.estatus IS NULL OR estatus <> 'B');";

            const string empresasQuery = @"SELECT e.empresa_id, e.nombre, e.rfc
                                            FROM banquetes.usuarios_empresas ue
                                            INNER JOIN banquetes.empresas e ON e.empresa_id = ue.empresa_id
                                            WHERE ue.usuario_id = @usuarioId
                                              AND (ue.estatus IS NULL OR ue.estatus <> 'B');";

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
                            role = reader["rol_nombre"].ToString();
                            nombreuser = reader["usuario_nombre"].ToString();
                            usuarioid = reader["usuario_id"].ToString();
                        }
                        else
                        {
                            return false;
                        }
                    }

                    using (var empresasCommand = new MySqlCommand(empresasQuery, connection))
                    {
                        empresasCommand.Parameters.AddWithValue("@usuarioId", usuarioid);
                        using (var empresasReader = empresasCommand.ExecuteReader())
                        {
                            while (empresasReader.Read())
                            {
                                var empresa = new Empresa
                                {
                                    Id = empresasReader.GetInt32("empresa_id"),
                                    Nombre = empresasReader.GetString("nombre"),
                                    Rfc = empresasReader.IsDBNull(empresasReader.GetOrdinal("rfc"))
                                        ? string.Empty
                                        : empresasReader.GetString("rfc")
                                };
                                empresas.Add(empresa);
                            }
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al validar las credenciales de usuario", ex);
            }
        }
    }
}

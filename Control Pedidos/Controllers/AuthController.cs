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
            // Guardamos la fábrica para sacar conexiones limpias cada vez que hagamos login u otra consulta.
            _connectionFactory = connectionFactory;
        }

        public bool TryLogin(string username, string password, out string role, out string nombreuser, out string usuarioid)
        {
            role = string.Empty;
            nombreuser = string.Empty;
            usuarioid = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                // Si mandan datos vacíos ni nos molestamos en ir a la base.
                return false;
            }

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
                            // Si hay coincidencia, rellenamos la info que espera la UI.
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
                // Envuelve el error original para que la capa superior pueda mostrar algo decente.
                throw new InvalidOperationException("Error al validar las credenciales de usuario", ex);
            }

            return false;
        }

        public IList<Empresa> GetEmpresasPorUsuario(string usuarioId)
        {
            var empresas = new List<Empresa>();

            const string query = @"SELECT e.empresa_id, e.nombre AS empresa_nombre, e.rfc AS empresa_rfc
                                    FROM banquetes.usuarios_empresas ue
                                    INNER JOIN banquetes.empresas e ON ue.empresa_id = e.empresa_id
                                    WHERE ue.usuario_id = @usuarioId
                                      AND (ue.estatus IS NULL OR ue.estatus <> 'B');";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@usuarioId", usuarioId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Construimos la lista de empresas habilitadas para el usuario.
                        empresas.Add(new Empresa
                        {
                            Id = reader.GetInt32("empresa_id"),
                            Nombre = reader.GetString("empresa_nombre"),
                            Rfc = reader.IsDBNull(reader.GetOrdinal("empresa_rfc")) ? string.Empty : reader.GetString("empresa_rfc")
                        });
                    }
                }
            }

            return empresas;
        }
    }
}

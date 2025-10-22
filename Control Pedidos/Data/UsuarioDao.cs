using System;
using System.Collections.Generic;
using System.Data;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// Maneja las operaciones de persistencia para usuarios y sus roles.
    /// </summary>
    public class UsuarioDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public UsuarioDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool Agregar(Usuario usuario, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        const string insertUser = @"INSERT INTO usuarios (nombre, correo, pass, rol_usuario_id, estatus, fecha_creacion)
VALUES (@nombre, @correo, @password, @rolUsuarioId, @estatus, @fechaCreacion);";

                        using (var command = new MySqlCommand(insertUser, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                            command.Parameters.AddWithValue("@correo", usuario.Correo);
                            command.Parameters.AddWithValue("@password", usuario.PasswordHash);
                            command.Parameters.AddWithValue("@rolUsuarioId", usuario.RolUsuarioId.HasValue ? (object)usuario.RolUsuarioId.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@estatus", string.IsNullOrWhiteSpace(usuario.Estatus) ? "N" : usuario.Estatus);
                            command.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);

                            command.ExecuteNonQuery();
                            usuario.Id = Convert.ToInt32(command.LastInsertedId);
                        }

                        transaction.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo agregar el usuario: {ex.Message}";
                return false;
            }
        }

        public bool Actualizar(Usuario usuario, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        const string updateUser = @"UPDATE usuarios
SET nombre = @nombre,
    correo = @correo,
    pass = COALESCE(@password, pass),
    rol_usuario_id = @rolUsuarioId,
    estatus = @estatus
WHERE usuario_id = @usuarioId;";

                        using (var command = new MySqlCommand(updateUser, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                            command.Parameters.AddWithValue("@correo", usuario.Correo);
                            command.Parameters.AddWithValue("@password", string.IsNullOrEmpty(usuario.PasswordHash) ? (object)DBNull.Value : usuario.PasswordHash);
                            command.Parameters.AddWithValue("@rolUsuarioId", usuario.RolUsuarioId.HasValue ? (object)usuario.RolUsuarioId.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@estatus", string.IsNullOrWhiteSpace(usuario.Estatus) ? "N" : usuario.Estatus);
                            command.Parameters.AddWithValue("@usuarioId", usuario.Id);

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo actualizar el usuario: {ex.Message}";
                return false;
            }
        }

        public bool Eliminar(int usuarioId, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"UPDATE usuarios
SET estatus = 'B', fecha_baja = @fechaBaja
WHERE usuario_id = @usuarioId;", connection))
                {
                    command.Parameters.AddWithValue("@fechaBaja", DateTime.Now);
                    command.Parameters.AddWithValue("@usuarioId", usuarioId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo eliminar el usuario: {ex.Message}";
                return false;
            }
        }

        public IList<Usuario> Listar(string filtro)
        {
            var usuarios = new List<Usuario>();

            const string query = @"SELECT u.usuario_id, u.nombre, u.correo, u.rol_usuario_id, ru.nombre AS rol_nombre, u.estatus, u.fecha_creacion, u.fecha_baja
FROM usuarios u
LEFT JOIN roles_usuarios ru ON ru.rol_usuario_id = u.rol_usuario_id
WHERE (@filtro = '' OR u.nombre LIKE CONCAT('%', @filtro, '%') OR u.correo LIKE CONCAT('%', @filtro, '%'))";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Nombre = reader.GetString("nombre"),
                                Correo = reader.GetString("correo"),
                                RolUsuarioId = reader.IsDBNull(reader.GetOrdinal("rol_usuario_id")) ? (int?)null : reader.GetInt32("rol_usuario_id"),
                                RolNombre = reader.IsDBNull(reader.GetOrdinal("rol_nombre")) ? string.Empty : reader.GetString("rol_nombre"),
                                Estatus = reader.GetString("estatus").ToUpperInvariant(),
                                FechaCreacion = reader.IsDBNull(reader.GetOrdinal("fecha_creacion")) ? (DateTime?)null : reader.GetDateTime("fecha_creacion"),
                                FechaBaja = reader.IsDBNull(reader.GetOrdinal("fecha_baja")) ? (DateTime?)null : reader.GetDateTime("fecha_baja")
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudo listar los usuarios", ex);
            }

            return usuarios;
        }
    }
}

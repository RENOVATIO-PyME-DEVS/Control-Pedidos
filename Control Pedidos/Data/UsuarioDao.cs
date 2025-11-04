using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            // Guardamos la fábrica para abrir conexiones en cada operación.
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool Agregar(Usuario usuario, IList<int> roles, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        const string insertUser = @"INSERT INTO usuarios (nombre, correo, password, rol_usuario_id, estatus, fecha_creacion)
VALUES (@nombre, @correo, @password, @rolUsuarioId, @estatus, @fechaCreacion);";

                        using (var command = new MySqlCommand(insertUser, connection, transaction))
                        {
                            // Datos básicos del usuario nuevo, incluyendo hash ya generado.
                            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                            command.Parameters.AddWithValue("@correo", usuario.Correo);
                            command.Parameters.AddWithValue("@password", usuario.PasswordHash);
                            command.Parameters.AddWithValue("@rolUsuarioId", usuario.RolUsuarioId.HasValue ? (object)usuario.RolUsuarioId.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@estatus", usuario.Estatus);
                            command.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);

                            command.ExecuteNonQuery();
                            usuario.Id = Convert.ToInt32(command.LastInsertedId);
                        }

                        // Guardamos los roles asociados aprovechando la misma transacción.
                        PersistirRoles(connection, transaction, usuario.Id, roles);

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

        public bool Actualizar(Usuario usuario, IList<int> roles, out string message)
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
    password = COALESCE(@password, password),
    rol_usuario_id = @rolUsuarioId,
    estatus = @estatus
WHERE usuario_id = @usuarioId;";

                        using (var command = new MySqlCommand(updateUser, connection, transaction))
                        {
                            // Si no viene password nuevo dejamos el que ya estaba en la base.
                            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                            command.Parameters.AddWithValue("@correo", usuario.Correo);
                            command.Parameters.AddWithValue("@password", string.IsNullOrEmpty(usuario.PasswordHash) ? (object)DBNull.Value : usuario.PasswordHash);
                            command.Parameters.AddWithValue("@rolUsuarioId", usuario.RolUsuarioId.HasValue ? (object)usuario.RolUsuarioId.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@estatus", usuario.Estatus);
                            command.Parameters.AddWithValue("@usuarioId", usuario.Id);

                            command.ExecuteNonQuery();
                        }

                        // Primero limpiamos los roles actuales y luego cargamos los nuevos.
                        const string deleteRoles = "DELETE FROM usuarios_roles WHERE usuario_id = @usuarioId";
                        using (var deleteCommand = new MySqlCommand(deleteRoles, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@usuarioId", usuario.Id);
                            deleteCommand.ExecuteNonQuery();
                        }

                        PersistirRoles(connection, transaction, usuario.Id, roles);

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
SET estatus = 'Inactivo', fecha_baja = @fechaBaja
WHERE usuario_id = @usuarioId;", connection))
                {
                    // Marcamos fecha de baja para tener historial pero sin borrar el registro.
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

            const string query = @"SELECT u.usuario_id, u.nombre, u.correo, u.rol_usuario_id, u.estatus, u.fecha_creacion, u.fecha_baja
FROM usuarios u
WHERE (@filtro = '' OR u.nombre LIKE CONCAT('%', @filtro, '%') OR u.correo LIKE CONCAT('%', @filtro, '%'))";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    // El filtro también es opcional acá, por nombre o correo.
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
                                Estatus = reader.GetString("estatus"),
                                FechaCreacion = reader.IsDBNull(reader.GetOrdinal("fecha_creacion")) ? (DateTime?)null : reader.GetDateTime("fecha_creacion"),
                                FechaBaja = reader.IsDBNull(reader.GetOrdinal("fecha_baja")) ? (DateTime?)null : reader.GetDateTime("fecha_baja")
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }

                // Una vez cargados los usuarios, recuperamos sus roles asociados.
                CargarRoles(usuarios);
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudo listar los usuarios", ex);
            }

            return usuarios;
        }

        private void PersistirRoles(MySqlConnection connection, MySqlTransaction transaction, int usuarioId, IEnumerable<int> roles)
        {
            if (roles == null)
            {
                return;
            }

            const string insertRole = "INSERT INTO usuarios_roles (usuario_id, rol_id) VALUES (@usuarioId, @rolId)";

            foreach (var rolId in roles.Distinct())
            {
                using (var command = new MySqlCommand(insertRole, connection, transaction))
                {
                    // Insertamos rol por rol evitando duplicados con Distinct por las dudas.
                    command.Parameters.AddWithValue("@usuarioId", usuarioId);
                    command.Parameters.AddWithValue("@rolId", rolId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CargarRoles(IList<Usuario> usuarios)
        {
            if (usuarios == null || usuarios.Count == 0)
            {
                return;
            }

            // Armamos una lista de ids para traer todos los roles en un solo viaje.
            var usuariosIds = string.Join(",", usuarios.Select(u => u.Id));
            var rolesPorUsuario = new Dictionary<int, List<Rol>>();

            var query = $@"SELECT ur.usuario_id, r.rol_id, r.nombre, r.descripcion, r.estatus
FROM usuarios_roles ur
INNER JOIN roles r ON r.rol_id = ur.rol_id
WHERE ur.usuario_id IN ({usuariosIds})";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuarioId = reader.GetInt32("usuario_id");
                        if (!rolesPorUsuario.TryGetValue(usuarioId, out var listaRoles))
                        {
                            listaRoles = new List<Rol>();
                            rolesPorUsuario[usuarioId] = listaRoles;
                        }

                        listaRoles.Add(new Rol
                        {
                            Id = reader.GetInt32("rol_id"),
                            Nombre = reader.GetString("nombre"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion"),
                            Estatus = reader.GetString("estatus")
                        });
                    }
                }
            }

            foreach (var usuario in usuarios)
            {
                if (rolesPorUsuario.TryGetValue(usuario.Id, out var listaRoles))
                {
                    usuario.Roles = listaRoles;
                }
            }
        }
    }
}

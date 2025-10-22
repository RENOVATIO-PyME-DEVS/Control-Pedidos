using System;
using System.Collections.Generic;
using System.Data;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// Acceso a datos para la entidad Rol.
    /// </summary>
    public class RolDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public RolDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool Agregar(Rol rol, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"INSERT INTO roles_usuarios (nombre)
VALUES (@nombre);", connection))
                {
                    command.Parameters.AddWithValue("@nombre", rol.Nombre);

                    connection.Open();
                    command.ExecuteNonQuery();
                    rol.Id = Convert.ToInt32(command.LastInsertedId);
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo agregar el rol: {ex.Message}";
                return false;
            }
        }

        public bool Actualizar(Rol rol, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"UPDATE roles_usuarios SET nombre = @nombre WHERE rol_usuario_id = @rolId;", connection))
                {
                    command.Parameters.AddWithValue("@nombre", rol.Nombre);
                    command.Parameters.AddWithValue("@rolId", rol.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo actualizar el rol: {ex.Message}";
                return false;
            }
        }

        public bool Eliminar(int rolId, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"DELETE FROM roles_usuarios WHERE rol_usuario_id = @rolId;", connection))
                {
                    command.Parameters.AddWithValue("@rolId", rolId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo eliminar el rol: {ex.Message}";
                return false;
            }
        }

        public IList<Rol> Listar()
        {
            var roles = new List<Rol>();
            const string query = "SELECT rol_usuario_id, nombre FROM roles_usuarios";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Rol
                            {
                                Id = reader.GetInt32("rol_usuario_id"),
                                Nombre = reader.GetString("nombre")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron listar los roles", ex);
            }

            return roles;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// Acceso a datos para clientes.
    /// </summary>
    public class ClienteDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public ClienteDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool Agregar(Cliente cliente, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"INSERT INTO clientes (nombre, razon_social, rfc, telefono, correo, direccion, estatus)
VALUES (@nombre, @razonSocial, @rfc, @telefono, @correo, @direccion, @estatus);", connection))
                {
                    command.Parameters.AddWithValue("@nombre", cliente.NombreComercial);
                    command.Parameters.AddWithValue("@razonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@rfc", cliente.Rfc);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@correo", cliente.Correo);
                    command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@estatus", cliente.Estatus);

                    connection.Open();
                    command.ExecuteNonQuery();
                    cliente.Id = Convert.ToInt32(command.LastInsertedId);
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo agregar el cliente: {ex.Message}";
                return false;
            }
        }

        public bool Actualizar(Cliente cliente, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"UPDATE clientes SET nombre = @nombre, razon_social = @razonSocial, rfc = @rfc, telefono = @telefono, correo = @correo, direccion = @direccion, estatus = @estatus WHERE cliente_id = @clienteId;", connection))
                {
                    command.Parameters.AddWithValue("@nombre", cliente.NombreComercial);
                    command.Parameters.AddWithValue("@razonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@rfc", cliente.Rfc);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@correo", cliente.Correo);
                    command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@estatus", cliente.Estatus);
                    command.Parameters.AddWithValue("@clienteId", cliente.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo actualizar el cliente: {ex.Message}";
                return false;
            }
        }

        public bool Eliminar(int clienteId, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"UPDATE clientes SET estatus = 'Inactivo' WHERE cliente_id = @clienteId;", connection))
                {
                    command.Parameters.AddWithValue("@clienteId", clienteId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo eliminar el cliente: {ex.Message}";
                return false;
            }
        }

        public IList<Cliente> Listar(string filtro)
        {
            var clientes = new List<Cliente>();
            const string query = @"SELECT cliente_id, nombre, razon_social, rfc, telefono, correo, direccion, estatus
FROM clientes
WHERE (@filtro = '' OR nombre LIKE CONCAT('%', @filtro, '%') OR rfc LIKE CONCAT('%', @filtro, '%'))";

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
                            clientes.Add(new Cliente
                            {
                                Id = reader.GetInt32("cliente_id"),
                                NombreComercial = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                                RazonSocial = reader.IsDBNull(reader.GetOrdinal("razon_social")) ? string.Empty : reader.GetString("razon_social"),
                                Rfc = reader.IsDBNull(reader.GetOrdinal("rfc")) ? string.Empty : reader.GetString("rfc"),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? string.Empty : reader.GetString("telefono"),
                                Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? string.Empty : reader.GetString("correo"),
                                Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? string.Empty : reader.GetString("direccion"),
                                Estatus = reader.IsDBNull(reader.GetOrdinal("estatus")) ? string.Empty : reader.GetString("estatus")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron listar los clientes", ex);
            }

            return clientes;
        }
    }
}

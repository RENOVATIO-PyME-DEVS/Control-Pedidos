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
                //using (var command = new MySqlCommand(@"INSERT INTO clientes (nombre, razon_social, rfc, telefono, correo, direccion, estatus) VALUES (@nombre, @razonSocial, @rfc, @telefono, @correo, @direccion, @estatus);", connection))
                using (var command = new MySqlCommand(@"INSERT INTO clientes
(nombre, rfc, telefono, correo, estatus, codigo_postal, c_regimenfiscal_id)
VALUES (@nombre, @rfc, @telefono, @correo, @estatus, @codigoPostal, @regimenFiscalId);", connection))
                {
                    command.Parameters.AddWithValue("@nombre", cliente.NombreComercial);
                    //command.Parameters.AddWithValue("@razonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@rfc", string.IsNullOrWhiteSpace(cliente.Rfc) ? (object)DBNull.Value : cliente.Rfc);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@correo", cliente.Correo);
                    //command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@estatus", string.Equals(cliente.Estatus, "Activo", StringComparison.OrdinalIgnoreCase) ? "N" : "B");
                    command.Parameters.AddWithValue("@codigoPostal", string.IsNullOrWhiteSpace(cliente.CodigoPostal) ? (object)DBNull.Value : cliente.CodigoPostal);
                    command.Parameters.AddWithValue("@regimenFiscalId", cliente.RegimenFiscalId.HasValue ? (object)cliente.RegimenFiscalId.Value : DBNull.Value);

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
                //using (var command = new MySqlCommand(@"UPDATE clientes SET nombre = @nombre, razon_social = @razonSocial, rfc = @rfc, telefono = @telefono, correo = @correo, direccion = @direccion, estatus = @estatus WHERE cliente_id = @clienteId;", connection))
                using (var command = new MySqlCommand(@"UPDATE clientes SET nombre = @nombre, rfc = @rfc, telefono = @telefono, correo = @correo, estatus = @estatus, codigo_postal = @codigoPostal, c_regimenfiscal_id = @regimenFiscalId WHERE cliente_id = @clienteId;", connection))
                {
                    command.Parameters.AddWithValue("@nombre", cliente.NombreComercial);
                    //command.Parameters.AddWithValue("@razonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@rfc", string.IsNullOrWhiteSpace(cliente.Rfc) ? (object)DBNull.Value : cliente.Rfc);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@correo", cliente.Correo);
                    //command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@estatus", string.Equals(cliente.Estatus, "Activo", StringComparison.OrdinalIgnoreCase) ? "N" : "B");
                    command.Parameters.AddWithValue("@codigoPostal", string.IsNullOrWhiteSpace(cliente.CodigoPostal) ? (object)DBNull.Value : cliente.CodigoPostal);
                    //command.Parameters.AddWithValue("@requiereFactura", cliente.RequiereFactura ? "S" : "N");
                    command.Parameters.AddWithValue("@regimenFiscalId", cliente.RegimenFiscalId.HasValue ? (object)cliente.RegimenFiscalId.Value : DBNull.Value);
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
                using (var command = new MySqlCommand(@"UPDATE clientes SET estatus = 'B' WHERE cliente_id = @clienteId;", connection))
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
            const string query = @"SELECT c.cliente_id
	            , c.nombre
                , c.rfc
                , c.telefono
                , c.correo
                , c.codigo_postal
                ,  case 
		            when c.c_regimenfiscal_id is null THEN 'No'
                    ELSE 'Si'
	              end as requiere_factura
                , c.c_regimenfiscal_id,
                    case
                        when c.estatus = 'N' THEN 'Activo'
                        when c.estatus = 'P' THEN 'Pendiente'
                        when c.estatus = 'B' THEN 'Inactivo'
                    end as estatus
            , rf.descripcion regimen_nombre
                FROM banquetes.clientes c
            left join c_regimenfiscal rf on rf.c_regimenfiscal_id = c.c_regimenfiscal_id
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
                                Rfc = reader.IsDBNull(reader.GetOrdinal("rfc")) ? string.Empty : reader.GetString("rfc"),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? string.Empty : reader.GetString("telefono"),
                                Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? string.Empty : reader.GetString("correo"),
                                Estatus = reader.IsDBNull(reader.GetOrdinal("estatus")) ? string.Empty : reader.GetString("estatus"),
                                CodigoPostal = reader.IsDBNull(reader.GetOrdinal("codigo_postal")) ? string.Empty : reader.GetString("codigo_postal"),
                                RequiereFacturaStr = reader.IsDBNull(reader.GetOrdinal("requiere_factura")) ? string.Empty : reader.GetString("requiere_factura"),
                                RequiereFactura = (reader.GetString("requiere_factura") == "Si") ? true : false,
                                RegimenFiscalId = reader.IsDBNull(reader.GetOrdinal("c_regimenfiscal_id")) ? (int?)null : reader.GetInt32("c_regimenfiscal_id")
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

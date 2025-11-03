using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// Acceso a datos para artículos y kits.
    /// </summary>
    public class ArticuloDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public ArticuloDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool Agregar(Articulo articulo, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        const string insertQuery = @"INSERT INTO articulos (nombre, nombre_corto, tipo_articulo, unidad_medida, unidad_control, contenido_control, precio, fecha_precio, usuario_precio_id, estatus, personas)
VALUES (@nombre, @nombreCorto, @tipoArticulo, @unidadMedida, @unidadControl, @contenidoControl, @precio, @fechaPrecio, @usuarioPrecioId, @estatus, @personas);";

                        using (var command = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@nombre", articulo.Nombre);
                            command.Parameters.AddWithValue("@nombreCorto", articulo.NombreCorto);
                            command.Parameters.AddWithValue("@tipoArticulo", articulo.TipoArticulo);
                            command.Parameters.AddWithValue("@unidadMedida", articulo.UnidadMedida);
                            command.Parameters.AddWithValue("@unidadControl", articulo.UnidadControl);
                            command.Parameters.AddWithValue("@contenidoControl", articulo.ContenidoControl);
                            command.Parameters.AddWithValue("@precio", articulo.Precio);
                            command.Parameters.AddWithValue("@fechaPrecio", articulo.FechaPrecio);
                            command.Parameters.AddWithValue("@usuarioPrecioId", articulo.UsuarioPrecioId.HasValue ? (object)articulo.UsuarioPrecioId.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@estatus", articulo.Estatus);
                            command.Parameters.AddWithValue("@personas", articulo.Personas);

                            command.ExecuteNonQuery();
                            articulo.Id = Convert.ToInt32(command.LastInsertedId);
                        }

                        if (articulo.EsKit)
                        {
                            GuardarComponentes(connection, transaction, articulo);
                        }

                        transaction.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo agregar el artículo: {ex.Message}";
                return false;
            }
        }

        public bool Actualizar(Articulo articulo, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        const string updateQuery = @"UPDATE articulos
SET nombre = @nombre,
    nombre_corto = @nombreCorto,
    tipo_articulo = @tipoArticulo,
    unidad_medida = @unidadMedida,
    unidad_control = @unidadControl,
    contenido_control = @contenidoControl,
    precio = @precio,
    fecha_precio = @fechaPrecio,
    usuario_precio_id = @usuarioPrecioId,
    estatus = @estatus,
    personas = @personas
WHERE articulo_id = @articuloId;";

                        using (var command = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@nombre", articulo.Nombre);
                            command.Parameters.AddWithValue("@nombreCorto", articulo.NombreCorto);
                            command.Parameters.AddWithValue("@tipoArticulo", articulo.TipoArticulo);
                            command.Parameters.AddWithValue("@unidadMedida", articulo.UnidadMedida);
                            command.Parameters.AddWithValue("@unidadControl", articulo.UnidadControl);
                            command.Parameters.AddWithValue("@contenidoControl", articulo.ContenidoControl);
                            command.Parameters.AddWithValue("@precio", articulo.Precio);
                            command.Parameters.AddWithValue("@fechaPrecio", articulo.FechaPrecio);
                            command.Parameters.AddWithValue("@usuarioPrecioId", articulo.UsuarioPrecioId.HasValue ? (object)articulo.UsuarioPrecioId.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@estatus", articulo.Estatus);
                            command.Parameters.AddWithValue("@tiene", articulo.Personas);
                            command.Parameters.AddWithValue("@articuloId", articulo.Id);

                            command.ExecuteNonQuery();
                        }

                        const string deleteDetalle = "DELETE FROM kit_detalle WHERE id_kit = @kitId";
                        using (var deleteCommand = new MySqlCommand(deleteDetalle, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@kitId", articulo.Id);
                            deleteCommand.ExecuteNonQuery();
                        }

                        if (articulo.EsKit)
                        {
                            GuardarComponentes(connection, transaction, articulo);
                        }

                        transaction.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo actualizar el artículo: {ex.Message}";
                return false;
            }
        }

        public bool Eliminar(int articuloId, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"UPDATE articulos SET estatus = 'Inactivo' WHERE articulo_id = @articuloId;", connection))
                {
                    command.Parameters.AddWithValue("@articuloId", articuloId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo eliminar el artículo: {ex.Message}";
                return false;
            }
        }

        public IList<Articulo> Listar(string filtro)
        {
            var articulos = new List<Articulo>();
            const string query = @"SELECT articulo_id, nombre, nombre_corto, tipo_articulo, unidad_medida, unidad_control, contenido_control, precio, fecha_precio, usuario_precio_id, estatus, coalesce(personas, 0) personas
FROM banquetes.articulos
WHERE (@filtro = '' OR nombre LIKE CONCAT('%', @filtro, '%'))";

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
                            var articulo = new Articulo
                            {
                                Id = reader.GetInt32("articulo_id"),
                                Nombre = reader.GetString("nombre"),
                                NombreCorto = reader.IsDBNull(reader.GetOrdinal("nombre_corto")) ? string.Empty : reader.GetString("nombre_corto"),
                                TipoArticulo = reader.GetString("tipo_articulo"),
                                UnidadMedida = reader.IsDBNull(reader.GetOrdinal("unidad_medida")) ? string.Empty : reader.GetString("unidad_medida"),
                                UnidadControl = reader.IsDBNull(reader.GetOrdinal("unidad_control")) ? string.Empty : reader.GetString("unidad_control"),
                                ContenidoControl = reader.IsDBNull(reader.GetOrdinal("contenido_control")) ? 0 : reader.GetDecimal("contenido_control"),
                                Precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0 : reader.GetDecimal("precio"),
                                FechaPrecio = reader.IsDBNull(reader.GetOrdinal("fecha_precio")) ? DateTime.Today : reader.GetDateTime("fecha_precio"),
                                UsuarioPrecioId = reader.IsDBNull(reader.GetOrdinal("usuario_precio_id")) ? (int?)null : reader.GetInt32("usuario_precio_id"),
                                Estatus = reader.GetString("estatus"),
                                Personas = reader.GetInt32("personas")
                            };

                            articulos.Add(articulo);
                        }
                    }
                }

                CargarComponentes(articulos.Where(a => a.EsKit).ToList());
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron listar los artículos", ex);
            }

            return articulos;
        }

        private void GuardarComponentes(MySqlConnection connection, MySqlTransaction transaction, Articulo articulo)
        {
            if (articulo.Componentes == null)
            {
                return;
            }

            const string insertDetalle = "INSERT INTO kit_detalle (id_kit, id_articulo, cantidad) VALUES (@kitId, @articuloId, @cantidad)";

            foreach (var detalle in articulo.Componentes)
            {
                using (var command = new MySqlCommand(insertDetalle, connection, transaction))
                {
                    command.Parameters.AddWithValue("@kitId", articulo.Id);
                    command.Parameters.AddWithValue("@articuloId", detalle.ArticuloId);
                    command.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CargarComponentes(IList<Articulo> kits)
        {
            if (kits == null || kits.Count == 0)
            {
                return;
            }

            var ids = string.Join(",", kits.Select(k => k.Id));
            var query = $@"SELECT kd.id_kit, kd.id_articulo, kd.cantidad, a.nombre, a.nombre_corto
FROM kit_detalle kd
INNER JOIN articulos a ON a.articulo_id = kd.id_articulo
WHERE kd.id_kit IN ({ids})";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var kitId = reader.GetInt32("id_kit");
                        var kit = kits.FirstOrDefault(k => k.Id == kitId);
                        if (kit == null)
                        {
                            continue;
                        }

                        kit.Componentes.Add(new KitDetalle
                        {
                            KitId = kitId,
                            ArticuloId = reader.GetInt32("id_articulo"),
                            Cantidad = reader.GetDecimal("cantidad"),
                            Articulo = new Articulo
                            {
                                Id = reader.GetInt32("id_articulo"),
                                Nombre = reader.GetString("nombre"),
                                NombreCorto = reader.IsDBNull(reader.GetOrdinal("nombre_corto")) ? string.Empty : reader.GetString("nombre_corto")
                            }
                        });
                    }
                }
            }
        }
    }
}

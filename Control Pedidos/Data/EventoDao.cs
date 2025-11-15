using System;
using System.Collections.Generic;
using System.Data;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    /// <summary>
    /// Acceso a datos para la administración de eventos.
    /// </summary>
    public class EventoDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public EventoDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IList<Evento> Listar(string filtro)
        {
            var eventos = new List<Evento>();
            const string query = @"SELECT e.evento_id,
                                           e.empresa_id,
                                           e.nombre,
                                           e.fecha_evento,
                                           e.tiene_serie,
                                           e.serie,
                                           e.siguiente_folio,
                                           em.nombre AS empresa_nombre,
                                           (SELECT COUNT(*)
                                              FROM banquetes.pedidos p
                                             WHERE p.evento_id = e.evento_id) AS pedidos_relacionados
                                      FROM banquetes.eventos e
                                      INNER JOIN banquetes.empresas em ON em.empresa_id = e.empresa_id
                                     WHERE (@filtro = '' OR e.nombre LIKE CONCAT('%', @filtro, '%'))
                                     ORDER BY e.fecha_evento DESC, e.nombre;";

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
                            eventos.Add(new Evento
                            {
                                Id = reader.GetInt32("evento_id"),
                                EmpresaId = reader.GetInt32("empresa_id"),
                                EmpresaNombre = reader.IsDBNull(reader.GetOrdinal("empresa_nombre")) ? string.Empty : reader.GetString("empresa_nombre"),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                                FechaEvento = reader.GetDateTime("fecha_evento"),
                                TieneSerie = reader.GetBoolean("tiene_serie"),
                                Serie = reader.IsDBNull(reader.GetOrdinal("serie")) ? string.Empty : reader.GetString("serie"),
                                SiguienteFolio = reader.GetInt32("siguiente_folio"),
                                TienePedidos = reader.GetInt32("pedidos_relacionados") > 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron listar los eventos", ex);
            }

            return eventos;
        }

        public bool Agregar(Evento evento, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"INSERT INTO banquetes.eventos
(empresa_id, nombre, fecha_evento, tiene_serie, serie, siguiente_folio)
VALUES (@empresaId, @nombre, @fechaEvento, @tieneSerie, @serie, @siguienteFolio);", connection))
                {
                    command.Parameters.AddWithValue("@empresaId", evento.EmpresaId);
                    command.Parameters.AddWithValue("@nombre", evento.Nombre);
                    command.Parameters.AddWithValue("@fechaEvento", evento.FechaEvento);
                    command.Parameters.AddWithValue("@tieneSerie", evento.TieneSerie);
                    command.Parameters.AddWithValue("@serie", evento.TieneSerie ? (object)evento.Serie : DBNull.Value);
                    command.Parameters.AddWithValue("@siguienteFolio", evento.SiguienteFolio);

                    connection.Open();
                    command.ExecuteNonQuery();
                    evento.Id = Convert.ToInt32(command.LastInsertedId);
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo agregar el evento: {ex.Message}";
                return false;
            }
        }

        public bool Actualizar(Evento evento, out string message)
        {
            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();

                    using (var validationCommand = new MySqlCommand(@"SELECT COUNT(*) FROM banquetes.pedidos WHERE evento_id = @eventoId;", connection))
                    {
                        validationCommand.Parameters.AddWithValue("@eventoId", evento.Id);
                        var relatedCount = Convert.ToInt32(validationCommand.ExecuteScalar());
                        if (relatedCount > 0)
                        {
                            message = "El evento está relacionado con pedidos y no puede modificarse.";
                            return false;
                        }
                    }

                    using (var command = new MySqlCommand(@"UPDATE banquetes.eventos
SET empresa_id = @empresaId,
    nombre = @nombre,
    fecha_evento = @fechaEvento,
    tiene_serie = @tieneSerie,
    serie = @serie,
    siguiente_folio = @siguienteFolio
WHERE evento_id = @eventoId;", connection))
                    {
                        command.Parameters.AddWithValue("@empresaId", evento.EmpresaId);
                        command.Parameters.AddWithValue("@nombre", evento.Nombre);
                        command.Parameters.AddWithValue("@fechaEvento", evento.FechaEvento);
                        command.Parameters.AddWithValue("@tieneSerie", evento.TieneSerie);
                        command.Parameters.AddWithValue("@serie", evento.TieneSerie ? (object)evento.Serie : DBNull.Value);
                        command.Parameters.AddWithValue("@siguienteFolio", evento.SiguienteFolio);
                        command.Parameters.AddWithValue("@eventoId", evento.Id);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo actualizar el evento: {ex.Message}";
                return false;
            }
        }

        public IList<Evento> ObtenerEventosActivosPorEmpresa(int empresaId)
        {
            var eventos = new List<Evento>();
            const string query = @"SELECT e.evento_id,
                                           e.empresa_id,
                                           e.nombre,
                                           e.fecha_evento,
                                           em.nombre AS empresa_nombre
                                      FROM banquetes.eventos e
                                      INNER JOIN banquetes.empresas em ON em.empresa_id = e.empresa_id
                                     WHERE e.empresa_id = @empresaId
                                       AND DATE(e.fecha_evento) >= CURDATE()
                                     ORDER BY e.fecha_evento, e.nombre;";

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@empresaId", empresaId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventos.Add(new Evento
                            {
                                Id = reader.GetInt32("evento_id"),
                                EmpresaId = reader.GetInt32("empresa_id"),
                                EmpresaNombre = reader.IsDBNull(reader.GetOrdinal("empresa_nombre")) ? string.Empty : reader.GetString("empresa_nombre"),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                                FechaEvento = reader.GetDateTime("fecha_evento")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("No se pudieron obtener los eventos activos de la empresa.", ex);
            }

            return eventos;
        }
    }
}

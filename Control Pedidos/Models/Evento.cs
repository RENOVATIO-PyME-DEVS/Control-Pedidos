using System;
using System.Collections.Generic;
using Control_Pedidos.Data;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa un evento configurado para la empresa.
    /// </summary>
    public class Evento
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNombre { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; } = DateTime.Today;
        public bool TieneSerie { get; set; }
        public string Serie { get; set; } = string.Empty;
        public int SiguienteFolio { get; set; } = 1;
        public bool TienePedidos { get; set; }

        public static IList<Evento> Listar(DatabaseConnectionFactory connectionFactory, string filtro)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new EventoDao(connectionFactory);
            return dao.Listar(filtro);
        }

        public static bool Agregar(DatabaseConnectionFactory connectionFactory, Evento evento, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (evento == null)
            {
                throw new ArgumentNullException(nameof(evento));
            }

            var dao = new EventoDao(connectionFactory);
            return dao.Agregar(evento, out message);
        }

        public static bool Actualizar(DatabaseConnectionFactory connectionFactory, Evento evento, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (evento == null)
            {
                throw new ArgumentNullException(nameof(evento));
            }

            var dao = new EventoDao(connectionFactory);
            return dao.Actualizar(evento, out message);
        }
    }
}

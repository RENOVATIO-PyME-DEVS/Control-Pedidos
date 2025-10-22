using System;
using System.Collections.Generic;
using Control_Pedidos.Data;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa un rol dentro del sistema.
    /// </summary>
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public static bool Agregar(DatabaseConnectionFactory connectionFactory, Rol rol, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (rol == null)
            {
                throw new ArgumentNullException(nameof(rol));
            }

            var dao = new RolDao(connectionFactory);
            return dao.Agregar(rol, out message);
        }

        public static bool Actualizar(DatabaseConnectionFactory connectionFactory, Rol rol, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (rol == null)
            {
                throw new ArgumentNullException(nameof(rol));
            }

            var dao = new RolDao(connectionFactory);
            return dao.Actualizar(rol, out message);
        }

        public static bool Eliminar(DatabaseConnectionFactory connectionFactory, int rolId, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new RolDao(connectionFactory);
            return dao.Eliminar(rolId, out message);
        }

        public static IList<Rol> Listar(DatabaseConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new RolDao(connectionFactory);
            return dao.Listar();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Control_Pedidos.Data;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa a un usuario dentro del sistema y expone operaciones básicas de mantenimiento.
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int? RolUsuarioId { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public IList<Rol> Roles { get; set; } = new List<Rol>();
        public string RolesResumen => Roles != null && Roles.Count > 0 ? string.Join(", ", Roles.Select(r => r.Nombre)) : "Sin roles";

        public static bool Agregar(DatabaseConnectionFactory connectionFactory, Usuario usuario, IEnumerable<int> roles, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            var dao = new UsuarioDao(connectionFactory);
            return dao.Agregar(usuario, roles?.ToList() ?? new List<int>(), out message);
        }

        public static bool Actualizar(DatabaseConnectionFactory connectionFactory, Usuario usuario, IEnumerable<int> roles, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            var dao = new UsuarioDao(connectionFactory);
            return dao.Actualizar(usuario, roles?.ToList() ?? new List<int>(), out message);
        }

        public static bool Eliminar(DatabaseConnectionFactory connectionFactory, int usuarioId, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new UsuarioDao(connectionFactory);
            return dao.Eliminar(usuarioId, out message);
        }

        public static IList<Usuario> Listar(DatabaseConnectionFactory connectionFactory, string filtro = "")
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new UsuarioDao(connectionFactory);
            return dao.Listar(filtro);
        }
    }
}

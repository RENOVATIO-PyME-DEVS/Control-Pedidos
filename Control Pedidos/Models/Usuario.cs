using System;
using System.Collections.Generic;
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
        public string RolNombre { get; set; } = string.Empty;
        public string Estatus { get; set; } = "N";
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }

        public string EstatusDescripcion
        {
            get
            {
                return Estatus switch
                {
                    "B" => "Baja",
                    _ => "Normal"
                };
            }
        }

        public static bool Agregar(DatabaseConnectionFactory connectionFactory, Usuario usuario, out string message)
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
            return dao.Agregar(usuario, out message);
        }

        public static bool Actualizar(DatabaseConnectionFactory connectionFactory, Usuario usuario, out string message)
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
            return dao.Actualizar(usuario, out message);
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

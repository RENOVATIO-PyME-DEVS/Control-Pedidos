namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa a un cliente del sistema.
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        /// <summary>
        /// Estatus según el catálogo de la base de datos (P = Pendiente, N = Normal/Activo, B = Baja/Inactivo).
        /// </summary>
        public string Estatus { get; set; } = "N";

        public string EstatusDescripcion
        {
            get
            {
                return Estatus switch
                {
                    "P" => "Pendiente",
                    "B" => "Baja",
                    _ => "Normal"
                };
            }
        }

        public static bool Agregar(Data.DatabaseConnectionFactory connectionFactory, Cliente cliente, out string message)
        {
            if (connectionFactory == null)
            {
                throw new System.ArgumentNullException(nameof(connectionFactory));
            }

            if (cliente == null)
            {
                throw new System.ArgumentNullException(nameof(cliente));
            }

            var dao = new Data.ClienteDao(connectionFactory);
            return dao.Agregar(cliente, out message);
        }

        public static bool Actualizar(Data.DatabaseConnectionFactory connectionFactory, Cliente cliente, out string message)
        {
            if (connectionFactory == null)
            {
                throw new System.ArgumentNullException(nameof(connectionFactory));
            }

            if (cliente == null)
            {
                throw new System.ArgumentNullException(nameof(cliente));
            }

            var dao = new Data.ClienteDao(connectionFactory);
            return dao.Actualizar(cliente, out message);
        }

        public static bool Eliminar(Data.DatabaseConnectionFactory connectionFactory, int clienteId, out string message)
        {
            if (connectionFactory == null)
            {
                throw new System.ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new Data.ClienteDao(connectionFactory);
            return dao.Eliminar(clienteId, out message);
        }

        public static System.Collections.Generic.IList<Cliente> Listar(Data.DatabaseConnectionFactory connectionFactory, string filtro)
        {
            if (connectionFactory == null)
            {
                throw new System.ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new Data.ClienteDao(connectionFactory);
            return dao.Listar(filtro);
        }
    }
}

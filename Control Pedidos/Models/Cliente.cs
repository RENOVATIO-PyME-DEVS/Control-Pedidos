namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa a un cliente del sistema.
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string NombreComercial { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Estatus { get; set; } = "Activo";
        public string CodigoPostal { get; set; } = string.Empty;
        public bool RequiereFactura { get; set; }
        public int? RegimenFiscalId { get; set; }
        public string Nombre
        {
            get => RazonSocial;
            set => RazonSocial = value;
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

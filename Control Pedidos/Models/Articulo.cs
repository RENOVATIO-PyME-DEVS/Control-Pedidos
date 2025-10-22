using System;
using System.Collections.Generic;
using Control_Pedidos.Data;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa un artículo o kit.
    /// </summary>
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public string TipoArticulo { get; set; } = "N";
        public string UnidadMedida { get; set; } = string.Empty;
        public string UnidadControl { get; set; } = string.Empty;
        public decimal ContenidoControl { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaPrecio { get; set; } = DateTime.Today;
        public int? UsuarioPrecioId { get; set; }
        public string Estatus { get; set; } = "N";
        public bool TieneInventario { get; set; }
        public IList<KitDetalle> Componentes { get; set; } = new List<KitDetalle>();

        public bool EsKit => string.Equals(TipoArticulo, "K", StringComparison.OrdinalIgnoreCase);

        public string TipoDescripcion
        {
            get
            {
                return TipoArticulo switch
                {
                    "K" => "Kit",
                    "P" => "Proceso",
                    _ => "Normal"
                };
            }
        }

        public string EstatusDescripcion
        {
            get
            {
                return Estatus switch
                {
                    "B" => "Baja",
                    "P" => "Pendiente",
                    _ => "Normal"
                };
            }
        }

        public static bool Agregar(DatabaseConnectionFactory connectionFactory, Articulo articulo, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (articulo == null)
            {
                throw new ArgumentNullException(nameof(articulo));
            }

            var dao = new ArticuloDao(connectionFactory);
            return dao.Agregar(articulo, out message);
        }

        public static bool Actualizar(DatabaseConnectionFactory connectionFactory, Articulo articulo, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            if (articulo == null)
            {
                throw new ArgumentNullException(nameof(articulo));
            }

            var dao = new ArticuloDao(connectionFactory);
            return dao.Actualizar(articulo, out message);
        }

        public static bool Eliminar(DatabaseConnectionFactory connectionFactory, int articuloId, out string message)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new ArticuloDao(connectionFactory);
            return dao.Eliminar(articuloId, out message);
        }

        public static IList<Articulo> Listar(DatabaseConnectionFactory connectionFactory, string filtro)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            var dao = new ArticuloDao(connectionFactory);
            return dao.Listar(filtro);
        }
    }
}

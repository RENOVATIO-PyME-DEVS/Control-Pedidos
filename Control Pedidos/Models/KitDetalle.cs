namespace Control_Pedidos.Models
{
    /// <summary>
    /// Detalle que relaciona un kit con sus artículos componentes.
    /// </summary>
    public class KitDetalle
    {
        public int KitId { get; set; }
        public int ArticuloId { get; set; }
        public decimal Cantidad { get; set; }
        public string Visible { get; set; } = "S";

        public Articulo Articulo { get; set; }
        public string NombreArticulo => Articulo?.Nombre ?? string.Empty;
    }
}

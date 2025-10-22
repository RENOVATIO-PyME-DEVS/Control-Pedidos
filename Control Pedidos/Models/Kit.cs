namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa un kit compuesto de múltiples artículos.
    /// </summary>
    public class Kit : Articulo
    {
        public Kit()
        {
            TipoArticulo = "kit";
        }
    }
}

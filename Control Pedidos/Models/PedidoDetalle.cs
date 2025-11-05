namespace Control_Pedidos.Models
{
    public class PedidoDetalle
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public Pedido Pedido { get; set; }
        public string ArticuloNombre => Articulo?.Nombre ?? string.Empty;
    }
}

using System;

namespace Control_Pedidos.Models
{
    public class CobroDetalle
    {
        public int PedidoId { get; set; }
        public string Folio { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public decimal Monto { get; set; }
    }
}

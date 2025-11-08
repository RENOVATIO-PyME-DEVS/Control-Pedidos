using System;

namespace Control_Pedidos.Models
{
    public class CobroDetalle
    {
        public int CobroId { get; set; }
        public int PedidoId { get; set; }
        public decimal Monto { get; set; }
    }
}

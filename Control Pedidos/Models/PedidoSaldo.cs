using System;

namespace Control_Pedidos.Models
{
    public class PedidoSaldo
    {
        public int PedidoId { get; set; }
        public string Folio { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }
        public decimal Abonado { get; set; }
        public decimal Saldo => Total - Abonado;
        public decimal MontoAsignado { get; set; }
    }
}

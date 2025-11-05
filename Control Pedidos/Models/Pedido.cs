using System;

namespace Control_Pedidos.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Folio { get; set; } = string.Empty;
        public string FolioFormateado { get; set; } = string.Empty;
        public Cliente Cliente { get; set; } = new Cliente();
        public Empresa Empresa { get; set; } = new Empresa();
        public Usuario Usuario { get; set; } = new Usuario();
        public Evento Evento { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntrega { get; set; }
        public TimeSpan? HoraEntrega { get; set; }
        public bool RequiereFactura { get; set; }
        public string Notas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }
        public System.Collections.Generic.List<PedidoDetalle> Detalles { get; set; } = new System.Collections.Generic.List<PedidoDetalle>();
    }
}

using System;

namespace Control_Pedidos.Models
{
    /*
     * Clase: Pedido
     * Descripción: Representa la orden de venta con toda la información necesaria para impresión y cálculos de saldo.
     */
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

        /// <summary>
        /// Importe total de los conceptos sin considerar descuentos.
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Monto total de la orden después de aplicar descuentos.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Monto abonado por el cliente a través de los cobros registrados.
        /// </summary>
        public decimal MontoAbonado { get; set; }

        /// <summary>
        /// Saldo pendiente a la fecha (Total - MontoAbonado).
        /// </summary>
        public decimal SaldoPendiente { get; set; }

        public decimal Descuento { get; set; }
        public string UsuarioDescuento { get; set; } = string.Empty;

        /// <summary>
        /// Forma de pago del último cobro registrado (si existe).
        /// </summary>
        public string FormaCobroUltima { get; set; } = string.Empty;

        public System.Collections.Generic.List<PedidoDetalle> Detalles { get; set; } = new System.Collections.Generic.List<PedidoDetalle>();
        public string Impreso { get; set; } = "N";
        public bool EstaImpreso => string.Equals(Impreso, "S", StringComparison.OrdinalIgnoreCase);
    }
}

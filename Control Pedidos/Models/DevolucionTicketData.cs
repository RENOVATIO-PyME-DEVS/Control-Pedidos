using System;

namespace Control_Pedidos.Models
{
    /*
     * Clase: DevolucionTicketData
     * Descripción: Contenedor de la información que se imprime en el comprobante de devolución.
     *              Incluye los datos del cliente, pedido, empresa y montos involucrados.
     */
    public class DevolucionTicketData
    {
        public string ClienteNombre { get; set; } = string.Empty;
        public string FolioPedido { get; set; } = string.Empty;
        public decimal TotalPedido { get; set; }
        public decimal MontoDevuelto { get; set; }
        public string FormaDevolucion { get; set; } = string.Empty;
        public DateTime FechaDevolucion { get; set; } = DateTime.Now;
        public Empresa Empresa { get; set; }
    }
}

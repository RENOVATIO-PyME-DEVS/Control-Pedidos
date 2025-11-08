using System;
using System.Collections.Generic;
using Control_Pedidos.Models;

namespace Control_Pedidos.Printing
{
    public class CobroTicketInfo
    {
        public Cobro Cobro { get; set; }
        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public string FormaCobroNombre { get; set; } = string.Empty;
        public IReadOnlyList<CobroTicketDetalle> Detalles { get; set; } = Array.Empty<CobroTicketDetalle>();
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoRestante { get; set; }
    }

    public class CobroTicketDetalle
    {
        public string Folio { get; set; } = string.Empty;
        public decimal Monto { get; set; }
    }
}

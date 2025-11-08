using System;
using System.Collections.Generic;

namespace Control_Pedidos.Models
{
    public class Cobro
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int FormaCobroId { get; set; }
        public string FormaCobroNombre { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoDespues { get; set; }
        public bool Impreso { get; set; }
        public IReadOnlyList<CobroDetalle> Detalles { get; set; } = Array.Empty<CobroDetalle>();
    }
}

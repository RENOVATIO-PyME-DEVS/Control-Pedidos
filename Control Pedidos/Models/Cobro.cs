using System;
using System.Collections.Generic;

namespace Control_Pedidos.Models
{
    public class Cobro
    {
        public int Id { get; set; }
        public int CobroPedidoId
        {
            get => Id;
            set => Id = value;
        }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int FormaCobroId { get; set; }
        public string FormaCobroNombre { get; set; } = string.Empty;
        public string FormaCobro
        {
            get => FormaCobroNombre;
            set => FormaCobroNombre = value;
        }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoDespues { get; set; }
        private string _impreso = "N";

        public string Impreso
        {
            get => string.IsNullOrWhiteSpace(_impreso) ? "N" : _impreso;
            set => _impreso = string.IsNullOrWhiteSpace(value) ? "N" : value.Trim().ToUpperInvariant();
        }

        public bool EstaImpreso => string.Equals(Impreso, "S", StringComparison.OrdinalIgnoreCase);

        public bool MostrarLeyendaCopia { get; set; }
        public IReadOnlyList<CobroDetalle> Detalles { get; set; } = Array.Empty<CobroDetalle>();
    }
}

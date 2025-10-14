using System;

namespace Control_Pedidos.Models
{
    public class CobroPedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int FormaCobroId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; } = string.Empty;
    }
}

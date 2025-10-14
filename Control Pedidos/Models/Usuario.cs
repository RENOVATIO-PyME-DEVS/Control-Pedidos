using System;

namespace Control_Pedidos.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public int? RolUsuarioId { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}

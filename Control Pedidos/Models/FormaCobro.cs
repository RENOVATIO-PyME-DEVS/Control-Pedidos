using System;

namespace Control_Pedidos.Models
{
    public class FormaCobro
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public override string ToString()
        {
            return Nombre;
        }
    }
}

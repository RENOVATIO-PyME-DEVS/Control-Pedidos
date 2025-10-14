namespace Control_Pedidos.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rfc { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Estatus { get; set; }
    }
}

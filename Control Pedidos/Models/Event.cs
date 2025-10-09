using System;

namespace Control_Pedidos.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Location { get; set; }
    }
}

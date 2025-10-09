using System;

namespace Control_Pedidos.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public Customer Customer { get; set; }
        public Event Event { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
    }
}

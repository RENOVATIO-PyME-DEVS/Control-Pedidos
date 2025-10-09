using System;

namespace Control_Pedidos.Models
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
    }
}

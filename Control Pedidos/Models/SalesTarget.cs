using System;

namespace Control_Pedidos.Models
{
    public class SalesTarget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Period { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal AchievedAmount { get; set; }
    }
}

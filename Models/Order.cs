﻿namespace Ecomm.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public Customer User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}

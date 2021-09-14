using System;

namespace ShopMart.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public long ProductId { get; set; }
        public string BoughtPrice { get; set; }
        public long OrderId { get; set; }

    }
}
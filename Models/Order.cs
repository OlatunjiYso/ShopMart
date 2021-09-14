using System;
using System.Collections.Generic;

namespace ShopMart.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public long CustomerAddressId { get; set; }
        public int OrderTotal { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
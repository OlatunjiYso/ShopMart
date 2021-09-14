using System;

namespace ShopMart.Models
{
    public class CartItem
    {
        public long CartItemId { get; set; }
        public long ProductId { get; set; }
        public int Quantity{ get; set; }

    }
}
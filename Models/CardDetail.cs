using System;

namespace ShopMart.Models
{
    public class CardDetail
    {
        public long CardDetailId { get; set; }
        public long CustomerId { get; set; }
        public string NameOnCard { get; set; }
        public string CardExpiryDate { get; set; }
        public string CardDigits {get; set; }
        public string CVV { get; set; }
    }
}
using System;

namespace ShopMart.Models
{
    public class CustomerPhone
    {
        public long CustomerPhoneId { get; set; }
        public long CustomerId { get; set; }
        public bool IsPrimary { get; set; }
        public string phoneNumber { get; set; }

    }
}
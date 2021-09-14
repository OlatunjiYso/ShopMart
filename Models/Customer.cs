using System;

namespace ShopMart.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password {get; set; }

    }
}
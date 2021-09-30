using System;
using System.ComponentModel.DataAnnotations;

namespace ShopMart.Models
{
    public class Customer
    {
        
        public long CustomerId { get; set; }
        [Required]
        public string  FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password {get; set; }

    }
}
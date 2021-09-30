using System;
using System.ComponentModel.DataAnnotations;

namespace ShopMart.DTO
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

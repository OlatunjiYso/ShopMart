using System;
namespace ShopMart.DTO
{
    public class RegistrationResponse
    {
        public string JwtToken { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}

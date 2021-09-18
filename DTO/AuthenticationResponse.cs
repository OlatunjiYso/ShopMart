using System;
namespace ShopMart.DTO
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }
        public string JwtToken { get; set; }
        public bool Success { get; set; }
    }
}

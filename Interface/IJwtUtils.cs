using System;
using ShopMart.DTO;
using ShopMart.Models;

namespace ShopMart.Interface
{
    public interface IJwtUtils
    {
        public string GenerateToken(Customer customer);
        public EncodedUser? ValidateToken(string token);
    }
}

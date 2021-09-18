using System;
using Microsoft.Extensions.Options;
using ShopMart.Interface;
using ShopMart.Models;
using ShopMart.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ShopMart.DTO;
using System.Linq;

namespace ShopMart.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        public string GenerateToken(Customer customer)
        {
            // generate token valid for 14 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", customer.CustomerId.ToString(), "Email", customer.Email) }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public EncodedUser? ValidateToken(string token)
        {
            // declare token claim variable
            EncodedUser encodedUser = new EncodedUser();
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                var email = jwtToken.Claims.First(x => x.Type == "Email").Value;
                encodedUser.Id = userId;
                encodedUser.Email = email;

                // return customer claim from JWT token if validation successful
                return encodedUser;
            }
            catch
            {
                // return null if validation fails
                return null;
            }

        }
    }
}

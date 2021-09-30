using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ShopMart.Helpers;
using ShopMart.Interface;

namespace ShopMart.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, ICustomerService customerService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var encodedUser = jwtUtils.ValidateToken(token);
            if(encodedUser != null)
            {
                context.Items["Customer"] = customerService.GetById(encodedUser.Id);
            }

            await _next(context);
        }
    }
}

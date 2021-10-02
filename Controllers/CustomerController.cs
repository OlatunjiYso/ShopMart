using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopMart.Authorization;
using ShopMart.Data;
using ShopMart.DTO;
using ShopMart.Helpers;
using ShopMart.Interface;
using ShopMart.Models;

namespace ShopMart.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private readonly AppSettings _appSettings;

        public CustomerController(ICustomerService customerService, IOptions<AppSettings> appSettings)
        {
            _customerService = customerService;
            _appSettings = appSettings.Value;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            var customers = await _customerService.GetAll();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(long id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null)
                return NotFound("Customer with specified ID doesnt exist.");
            return Ok(customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(long id, UpdateCustomerRequest customer)
        {
            await _customerService.UpdateCustomer(id, customer);
            return Ok(new { message = "User updated successfully" });
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult<RegistrationResponse>> RegisterCustomer(RegistrationRequest customer)
        {
            RegistrationResponse regResponse = await _customerService.Register(customer);
            if (regResponse.HasConflict)
            { return Conflict("Email: " + customer.Email + " has been taken already"); }
            return Ok(regResponse);
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateCustomer(AuthenticationRequest customer)
        {
            AuthenticationResponse authResponse = await _customerService.Authenticate(customer);
            if (!authResponse.Success)
            { return Unauthorized(authResponse); }

            return Ok(authResponse);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(long id)
        {
            _customerService.DeleteCustomer(id);
             return Ok(new { message = " Customer deleted successfully" });
        }
    }
}

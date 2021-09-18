using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopMart.Data;
using ShopMart.DTO;
using ShopMart.Helpers;
using ShopMart.Interface;
using ShopMart.Models;

namespace ShopMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ShopMartContext _context;
        private ICustomerService _customerService;
        private readonly AppSettings _appSettings;

        public CustomerController(ICustomerService customerService, IOptions<AppSettings> appSettings)
        {
            _customerService = customerService;
            _appSettings = appSettings.Value;
        }

        // GET: api/Customer
        [HttpGet]
        public ActionResult GetCustomers()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public ActionResult GetCustomer(long id)
        {
            var customer = _customerService.GetById(id);
            return Ok(customer);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  IActionResult PutCustomer(long id, UpdateCustomerRequest customer)
        {
            _customerService.UpdateCustomer(id, customer);
            return Ok(new { message = "User updated successfully" });
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostCustomer(RegistrationRequest customer)
        {
            _customerService.Register(customer);
            return Ok(new { message = "Registration successful" });
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(long id)
        {
            _customerService.DeleteCustomer(id);
            return Ok(new { message = " Customer deleted successfully" });
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        private static CustomerDTO customerToDTO(Customer customer)
        {
            var customerDTO = new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            return customerDTO;
        }
    }
}

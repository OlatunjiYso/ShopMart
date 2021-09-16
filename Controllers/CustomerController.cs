using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMart.Data;
using ShopMart.DTO;
using ShopMart.Models;

namespace ShopMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ShopMartContext _context;

        public CustomerController(ShopMartContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            return await _context.Customers
                .Select(x => customerToDTO(x))
                .ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customerToDTO(customer);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, CustomerDTO customerDTO)
        {
            if (id != customerDTO.CustomerId)
            {
                return BadRequest();
            }

            // _context.Entry(customer).State = EntityState.Modified;
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customerToDTO(customer));

        //    return CreatedAtAction(
        //nameof(GetCustomer),
        //new { customerId = customer.CustomerId },
        //customerToDTO(customer));
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
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

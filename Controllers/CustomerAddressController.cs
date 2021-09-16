using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMart.Data;
using ShopMart.Models;

namespace ShopMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ShopMartContext _context;

        public CustomerAddressController(ShopMartContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAddress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> GetCustumerAddresses()
        {
            return await _context.CustumerAddresses.ToListAsync();
        }

        // GET: api/CustomerAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> GetCustomerAddress(long id)
        {
            var customerAddress = await _context.CustumerAddresses.FindAsync(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return customerAddress;
        }

        // PUT: api/CustomerAddress/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAddress(long id, CustomerAddress customerAddress)
        {
            if (id != customerAddress.CustomerAddressId)
            {
                return BadRequest();
            }

            _context.Entry(customerAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
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

        // POST: api/CustomerAddress
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> PostCustomerAddress(CustomerAddress customerAddress)
        {
            _context.CustumerAddresses.Add(customerAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerAddress", new { id = customerAddress.CustomerAddressId }, customerAddress);
        }

        // DELETE: api/CustomerAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAddress(long id)
        {
            var customerAddress = await _context.CustumerAddresses.FindAsync(id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            _context.CustumerAddresses.Remove(customerAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerAddressExists(long id)
        {
            return _context.CustumerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}

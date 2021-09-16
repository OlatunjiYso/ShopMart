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
    public class CustomerPhoneController : ControllerBase
    {
        private readonly ShopMartContext _context;

        public CustomerPhoneController(ShopMartContext context)
        {
            _context = context;
        }

        // GET: api/CustomerPhone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPhone>>> GetCustomerPhone()
        {
            return await _context.CustomerPhone.ToListAsync();
        }

        // GET: api/CustomerPhone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPhone>> GetCustomerPhone(long id)
        {
            var customerPhone = await _context.CustomerPhone.FindAsync(id);

            if (customerPhone == null)
            {
                return NotFound();
            }

            return customerPhone;
        }

        // PUT: api/CustomerPhone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPhone(long id, CustomerPhone customerPhone)
        {
            if (id != customerPhone.CustomerPhoneId)
            {
                return BadRequest();
            }

            _context.Entry(customerPhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPhoneExists(id))
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

        // POST: api/CustomerPhone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerPhone>> PostCustomerPhone(CustomerPhone customerPhone)
        {
            _context.CustomerPhone.Add(customerPhone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerPhone", new { id = customerPhone.CustomerPhoneId }, customerPhone);
        }

        // DELETE: api/CustomerPhone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPhone(long id)
        {
            var customerPhone = await _context.CustomerPhone.FindAsync(id);
            if (customerPhone == null)
            {
                return NotFound();
            }

            _context.CustomerPhone.Remove(customerPhone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPhoneExists(long id)
        {
            return _context.CustomerPhone.Any(e => e.CustomerPhoneId == id);
        }
    }
}

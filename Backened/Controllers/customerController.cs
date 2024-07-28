using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicleservicesystmapi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vehicleservicesystmapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly PrimeAutomobilesDbContext _context;

        public CustomerController(PrimeAutomobilesDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers
                                 .Include(c => c.Vehicles)
                                 .ThenInclude(v => v.ServiceRecords)
                                 .ThenInclude(sr => sr.ServiceRepresentative)
                                 .Include(c => c.Vehicles)
                                 .ThenInclude(v => v.ServiceRecords)
                                 .ThenInclude(sr => sr.Invoice)
                                 .Include(c => c.Vehicles)
                                 .ThenInclude(v => v.ServiceRecords)
                                 .ThenInclude(sr => sr.BillOfMaterials)
                                 .ThenInclude(bom => bom.WorkItem)
                                 .ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                                         .Include(c => c.Vehicles)
                                         .ThenInclude(v => v.ServiceRecords)
                                         .ThenInclude(sr => sr.ServiceRepresentative)
                                         .Include(c => c.Vehicles)
                                         .ThenInclude(v => v.ServiceRecords)
                                         .ThenInclude(sr => sr.Invoice)
                                         .Include(c => c.Vehicles)
                                         .ThenInclude(v => v.ServiceRecords)
                                         .ThenInclude(sr => sr.BillOfMaterials)
                                         .ThenInclude(bom => bom.WorkItem)
                                         .FirstOrDefaultAsync(c => c.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure the nested relationships are correctly set
            if (customer.Vehicles != null)
            {
                foreach (var vehicle in customer.Vehicles)
                {
                    vehicle.Customer = customer;

                    if (vehicle.ServiceRecords != null)
                    {
                        foreach (var serviceRecord in vehicle.ServiceRecords)
                        {
                            serviceRecord.Vehicle = vehicle;

                            if (serviceRecord.Invoice != null)
                            {
                                serviceRecord.Invoice.ServiceRecord = serviceRecord;
                                _context.Invoices.Add(serviceRecord.Invoice); // Explicitly add invoice
                            }

                            if (serviceRecord.BillOfMaterials != null)
                            {
                                foreach (var billOfMaterial in serviceRecord.BillOfMaterials)
                                {
                                    billOfMaterial.ServiceRecord = serviceRecord;
                                    _context.BillOfMaterials.Add(billOfMaterial); // Explicitly add bill of materials
                                }
                            }

                            serviceRecord.ServiceRepresentative = await _context.ServiceRepresentatives.FindAsync(serviceRecord.ServiceRepresentativeID);
                        }
                    }
                }
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(customer).State = EntityState.Modified;

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

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
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

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}

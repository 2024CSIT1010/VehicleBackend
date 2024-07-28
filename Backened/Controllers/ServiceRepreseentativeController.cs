using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicleservicesystmapi.Models;

namespace vehicleservicesystmapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRepresentativeController : ControllerBase
    {
        private readonly PrimeAutomobilesDbContext _context;

        public ServiceRepresentativeController(PrimeAutomobilesDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceRepresentative
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRepresentative>>> GetServiceRepresentatives()
        {
            return await _context.ServiceRepresentatives.Include(sr => sr.ServiceRecords)
                                                       .ToListAsync();
        }

        // GET: api/ServiceRepresentative/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRepresentative>> GetServiceRepresentative(int id)
        {
            var serviceRepresentative = await _context.ServiceRepresentatives.Include(sr => sr.ServiceRecords)
                                                                            .FirstOrDefaultAsync(sr => sr.ServiceRepresentativeID == id);

            if (serviceRepresentative == null)
            {
                return NotFound();
            }

            return serviceRepresentative;
        }

        // POST: api/ServiceRepresentative
        [HttpPost]
        public async Task<ActionResult<ServiceRepresentative>> PostServiceRepresentative(ServiceRepresentative serviceRepresentative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ServiceRepresentatives.Add(serviceRepresentative);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceRepresentative), new { id = serviceRepresentative.ServiceRepresentativeID }, serviceRepresentative);
        }

        // PUT: api/ServiceRepresentative/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRepresentative(int id, ServiceRepresentative serviceRepresentative)
        {
            if (id != serviceRepresentative.ServiceRepresentativeID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(serviceRepresentative).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRepresentativeExists(id))
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

        // DELETE: api/ServiceRepresentative/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRepresentative(int id)
        {
            var serviceRepresentative = await _context.ServiceRepresentatives.FindAsync(id);
            if (serviceRepresentative == null)
            {
                return NotFound();
            }

            _context.ServiceRepresentatives.Remove(serviceRepresentative);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceRepresentativeExists(int id)
        {
            return _context.ServiceRepresentatives.Any(e => e.ServiceRepresentativeID == id);
        }
    }
}


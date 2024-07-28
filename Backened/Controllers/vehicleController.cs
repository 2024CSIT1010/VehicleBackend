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
    public class VehicleController : ControllerBase
    {
        private readonly PrimeAutomobilesDbContext _context;

        public VehicleController(PrimeAutomobilesDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.Include(v => v.Customer)
                                          .Include(v => v.ServiceRecords)
                                          .ToListAsync();
        }

        // GET: api/Vehicle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.Include(v => v.Customer)
                                                 .Include(v => v.ServiceRecords)
                                                 .FirstOrDefaultAsync(v => v.VehicleID == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // POST: api/Vehicle
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleID }, vehicle);
        }

        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VehicleID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleID == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicleservicesystmapi.Models;

namespace vehicleservicesystmapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRecordController : ControllerBase
    {
        private readonly PrimeAutomobilesDbContext _context;

        public ServiceRecordController(PrimeAutomobilesDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecord>>> GetServiceRecords()
        {
            return await _context.ServiceRecords
                .Include(sr => sr.Vehicle)
                .Include(sr => sr.ServiceRepresentative)
                .Include(sr => sr.BillOfMaterials)
                .Include(sr => sr.Invoice)
                .ToListAsync();
        }

        // GET: api/ServiceRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRecord>> GetServiceRecord(int id)
        {
            var serviceRecord = await _context.ServiceRecords
                .Include(sr => sr.Vehicle)
                .Include(sr => sr.ServiceRepresentative)
                .Include(sr => sr.BillOfMaterials)
                .Include(sr => sr.Invoice)
                .FirstOrDefaultAsync(sr => sr.ServiceRecordID == id);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            return serviceRecord;
        }

        // POST: api/ServiceRecord
        [HttpPost]
        public async Task<ActionResult<ServiceRecord>> PostServiceRecord(ServiceRecord serviceRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ServiceRecords.Add(serviceRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceRecord), new { id = serviceRecord.ServiceRecordID }, serviceRecord);
        }

        // PUT: api/ServiceRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRecord(int id, ServiceRecord serviceRecord)
        {
            if (id != serviceRecord.ServiceRecordID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(serviceRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRecordExists(id))
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

        // DELETE: api/ServiceRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRecord(int id)
        {
            var serviceRecord = await _context.ServiceRecords.FindAsync(id);
            if (serviceRecord == null)
            {
                return NotFound();
            }

            _context.ServiceRecords.Remove(serviceRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceRecordExists(int id)
        {
            return _context.ServiceRecords.Any(e => e.ServiceRecordID == id);
        }
    }
}

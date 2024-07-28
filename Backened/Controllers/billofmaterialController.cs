using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicleservicesystmapi.Models;

namespace vehicleservicesystmapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillOfMaterialController : ControllerBase
    {
        private readonly PrimeAutomobilesDbContext _context;

        public BillOfMaterialController(PrimeAutomobilesDbContext context)
        {
            _context = context;
        }

        // GET: api/BillOfMaterial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillOfMaterial>>> GetBillOfMaterials()
        {
            return await _context.BillOfMaterials
                .Include(b => b.ServiceRecord)
                .Include(b => b.WorkItem)
                .ToListAsync();
        }

        // GET: api/BillOfMaterial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillOfMaterial>> GetBillOfMaterial(int id)
        {
            var billOfMaterial = await _context.BillOfMaterials
                .Include(b => b.ServiceRecord)
                .Include(b => b.WorkItem)
                .FirstOrDefaultAsync(b => b.BillOfMaterialID == id);

            if (billOfMaterial == null)
            {
                return NotFound();
            }

            return billOfMaterial;
        }

        // POST: api/BillOfMaterial
        [HttpPost]
        public async Task<ActionResult<BillOfMaterial>> PostBillOfMaterial(BillOfMaterial billOfMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BillOfMaterials.Add(billOfMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBillOfMaterial), new { id = billOfMaterial.BillOfMaterialID }, billOfMaterial);
        }

        // PUT: api/BillOfMaterial/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillOfMaterial(int id, BillOfMaterial billOfMaterial)
        {
            if (id != billOfMaterial.BillOfMaterialID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(billOfMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillOfMaterialExists(id))
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

        // DELETE: api/BillOfMaterial/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillOfMaterial(int id)
        {
            var billOfMaterial = await _context.BillOfMaterials.FindAsync(id);
            if (billOfMaterial == null)
            {
                return NotFound();
            }

            _context.BillOfMaterials.Remove(billOfMaterial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillOfMaterialExists(int id)
        {
            return _context.BillOfMaterials.Any(e => e.BillOfMaterialID == id);
        }
    }
}

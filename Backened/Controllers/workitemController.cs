using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicleservicesystmapi.Models;

namespace vehicleservicesystmapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly PrimeAutomobilesDbContext _context;

        public WorkItemController(PrimeAutomobilesDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItems()
        {
            return await _context.WorkItems.Include(wi => wi.BillOfMaterials)
                                           .ToListAsync();
        }

        // GET: api/WorkItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(int id)
        {
            var workItem = await _context.WorkItems.Include(wi => wi.BillOfMaterials)
                                                   .FirstOrDefaultAsync(wi => wi.WorkItemID == id);

            if (workItem == null)
            {
                return NotFound();
            }

            return workItem;
        }

        // POST: api/WorkItem
        [HttpPost]
        public async Task<ActionResult<WorkItem>> PostWorkItem(WorkItem workItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkItems.Add(workItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkItem), new { id = workItem.WorkItemID }, workItem);
        }

        // PUT: api/WorkItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkItem(int id, WorkItem workItem)
        {
            if (id != workItem.WorkItemID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(workItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkItemExists(id))
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

        // DELETE: api/WorkItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkItem(int id)
        {
            var workItem = await _context.WorkItems.FindAsync(id);
            if (workItem == null)
            {
                return NotFound();
            }

            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkItemExists(int id)
        {
            return _context.WorkItems.Any(e => e.WorkItemID == id);
        }
    }
}

using _14548DSCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _14548DSCC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicController : ControllerBase
    {
        private readonly ModelContext _dbcontext;

        public ElectronicController(ModelContext dbcontext)
        {
            _dbcontext = dbcontext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Electronic>>> GetElectronic()
        {
            if (_dbcontext.Electronic == null)
            {
                return NotFound();
            }
            return await _dbcontext.Electronic.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Electronic>> GetElectronic(int id)
        {
            if (_dbcontext.Electronic == null)
            {
                return NotFound();
            }
            var electronic = await _dbcontext.Electronic.FindAsync(id);
            if (electronic == null)
            {
                return NotFound();
            }
            return electronic;
        }

        [HttpPost]
        public async Task<ActionResult<Electronic>> PostElectronic(Electronic electronic)
        {
            _dbcontext.Electronic.Add(electronic);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElectronic), new { id = electronic.Id }, electronic);
        }

        [HttpPut]
        public async Task<IActionResult> PutElectronic(int id, Electronic electronic)
        {
            if (id != electronic.Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(electronic).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectronicAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool ElectronicAvailable(int id)
        {
            return (_dbcontext. Electronic  ?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteElectronic(int id)
        {
            if (_dbcontext.Electronic == null)
            {
                return NotFound();
            }

            var electronic = await _dbcontext.Electronic.FindAsync(id);
            if (electronic == null)
            {
                return NotFound();
            }

            _dbcontext.Electronic.Remove(electronic);

            await _dbcontext.SaveChangesAsync();

            return Ok();
        }
    }
}

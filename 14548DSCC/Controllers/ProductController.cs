using _14548DSCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _14548DSCC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ModelContext _dbcontext;

        public ProductController(ModelContext dbcontext) 
        {
            _dbcontext = dbcontext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if(_dbcontext.Product == null)
            {
                return NotFound();
            }
            return await _dbcontext.Product.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_dbcontext.Product == null)
            {
                return NotFound();
            }
            var product = await _dbcontext.Product.FindAsync(id);
            if (product == null) { 
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _dbcontext.Product.Add(product);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(product).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            { 
              if(!ProductAvailable(id))
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

        private bool ProductAvailable(int id)
        {
            return (_dbcontext.Product?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(_dbcontext.Product == null)
            {
                return NotFound();
            }

            var product = await _dbcontext.Product.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            _dbcontext.Product.Remove(product);

            await _dbcontext.SaveChangesAsync();

            return Ok();
        }
    }
}

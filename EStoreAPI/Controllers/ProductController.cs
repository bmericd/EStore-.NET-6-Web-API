using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            
            return Ok(await _context.Products.ToListAsync()); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
                return BadRequest("Product not found.");
            return Ok(product); 
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            
             _context.Products.AddAsync(product);
             await _context.SaveChangesAsync();
            
            return Ok(await _context.Products.ToListAsync()); 
        }
        
        [HttpPut]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product updatedProduct)
        {
            
            var dbProduct = await _context.Products.FindAsync(updatedProduct.Id);
            if(dbProduct == null)
                return BadRequest("Product not found.");

            dbProduct.Id = updatedProduct.Id;
            dbProduct.Name = updatedProduct.Name;
            dbProduct.Price = updatedProduct.Price;

            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync()); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null)
                return BadRequest("Product not found.");

            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }
        
    }
}

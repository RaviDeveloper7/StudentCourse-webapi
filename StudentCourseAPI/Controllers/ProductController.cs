using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        //Get: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>GetProduct(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>>AddProduct(Product _Product)
        {
            _context.products.Add(_Product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), 
                new  { id = _Product.Id -1}, _Product );
        }

        [HttpPut]
        public async Task<ActionResult<Product>>UpdateProduct(int id , Product _product)
        {
            var product = await _context.products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            product.Name = _product.Name;
            product.Price = _product.Price;
            await _context.SaveChangesAsync();

            return product;
        }


        [HttpDelete]
        public async Task<ActionResult<Product>>DeleteProduct(int id)
        {
            var product = await _context.products.FindAsync(id);

            if(product==null)
            return NotFound();

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

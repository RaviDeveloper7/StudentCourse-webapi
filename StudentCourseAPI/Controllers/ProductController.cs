using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Exceptions;
using StudentCourseAPI.Models;
using StudentCourseAPI.Services;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        //Get: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
        {
            var product = await _service.GetAllAsync();
            return Ok(product);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with id {id} was not found.");


            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Add(ProductCreateDto _productCreateDto)
        {
            var created = await _service.CreateAsync(_productCreateDto);

            return CreatedAtAction(nameof(GetById),
                new { id = created.Id }, created);
        }


        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id,   ProductUpdateDto _productUpdateDto)
        {
            var updatedProduct = await _service.UpdateAsync(id , _productUpdateDto);

            if (!updatedProduct)
                return NotFound();
                
            return Ok(updatedProduct);
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

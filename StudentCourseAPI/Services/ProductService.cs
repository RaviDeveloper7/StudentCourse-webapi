using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService (AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _context.products.Select(
                p=> new ProductReadDto { Id = p.Id ,Name = p.Name, Price =p.Price}).ToListAsync();

            return products;
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
                return null;

            return new ProductReadDto { Id=product.Id,Name= product.Name, Price=product.Price };
        }

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = new Product
            { Name = productCreateDto.Name, Price = productCreateDto.Price };

            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductReadDto { Id = product.Id, Name = product.Name, Price = product.Price };
        }

        public async Task <bool> UpdateAsync(int id , ProductUpdateDto dto)
        {
            var product = await _context.products.FindAsync (id);

            if(product==null)
                return false;

            product.Name = dto.name;
            product.Price = dto.price;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool>DeleteAsync(int id)
        {
            var product = await _context.products.FindAsync(id);
            if(product==null) return false;

             _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return true;

        }

    }
}

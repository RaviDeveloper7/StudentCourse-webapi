using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;
using StudentCourseAPI.Repositories;

namespace StudentCourseAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            var createdDto = await _repository.AddAsync(product);

            return _mapper.Map<ProductReadDto>(createdDto);
        }

        public async Task<bool> UpdateAsync(int id, ProductUpdateDto productUpdateDto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);

            _mapper.Map(productUpdateDto, existingProduct);

            await _repository.UpdateAsync(existingProduct);

            var resultDto = _mapper.Map<ProductReadDto>(existingProduct);

            return true;        
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }
    }
}

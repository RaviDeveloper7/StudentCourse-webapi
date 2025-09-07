using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Helpers;
using StudentCourseAPI.Models;
using StudentCourseAPI.Repositories;
using System.Linq.Expressions;

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

        public async Task<PagedResult<ProductReadDto>> GetPagedProductsAsync(PaginationParams? pagination,
            string? filterOn = null, string? filterQuery = null)
        {
            var effectivePagination = pagination ?? new PaginationParams(); // Apply defaults

            Expression<Func<Product, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                predicate = filterOn.ToLower() switch
                {
                    "name" => p => p.Name.ToLower().Contains(filterQuery),
                    "price" => decimal.TryParse(filterQuery, out var price)
                             ? p => p.Price == price
                             : throw new ArgumentException("Invalid price filter value"),
                    _ => throw new ArgumentException($"Unknown filter field: {filterOn}")
                };
            }

            PagedResult<Product> pagedResult = predicate == null
                ? await _repository.GetAllAsync(effectivePagination)
                : await _repository.FindAsync(predicate, effectivePagination);

            return new PagedResult<ProductReadDto>
            {
                Items = _mapper.Map<IEnumerable<ProductReadDto>>(pagedResult.Items),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
    }
}
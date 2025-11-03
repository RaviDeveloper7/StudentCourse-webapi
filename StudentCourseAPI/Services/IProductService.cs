using StudentCourseAPI.DTOs;
using StudentCourseAPI.Helpers;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductReadDto>> GetPagedProductsAsync(PaginationParams? pagination, string? sortBy = null,
                                        string? sortOrder = "asc", string? filterOn = null, string? filterQuery = null);

        Task<ProductReadDto?>GetByIdAsync(int id);
        Task<ProductReadDto> CreateAsync(ProductCreateDto dto);

        Task<bool> UpdateAsync(int id, ProductUpdateDto dto);

        Task<bool> DeleteAsync(int id);

    }
}
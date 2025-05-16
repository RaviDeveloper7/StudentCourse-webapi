    using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllAsync();  
        Task<ProductReadDto?>GetByIdAsync(int id);
        Task<ProductReadDto> CreateAsync(ProductCreateDto dto);

        Task<bool> UpdateAsync(int id, ProductUpdateDto dto);

        Task<bool> DeleteAsync(int id);

    }
}

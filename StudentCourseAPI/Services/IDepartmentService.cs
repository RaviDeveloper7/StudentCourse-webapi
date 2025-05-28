using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentReadDto>> GetAllAsync();
        Task<DepartmentReadDto> GetByIdAsync(int id);
        Task<DepartmentReadDto> CreateAsync(DepartmentCreateDto Dto);
        Task<bool> UpdateAsync(int id ,DepartmentUpdateDto department);
        Task<bool> DeleteAsync(int id);
    }
}

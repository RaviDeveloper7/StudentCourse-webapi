using StudentCourseAPI.DTOs;

namespace StudentCourseAPI.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeReadDto>> GetAllAsync();

        Task<EmployeeReadDto> GetByIdAsync(int id);

        Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto employeeCreateDto);

        Task<bool> UpdateAsync(int id ,EmployeeUpdateDto employeeUpdateDto);

        Task<bool>DeleteAsync(int id);
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Services;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetById(int id)
        {
            var employee = await employeeService.GetByIdAsync(id);

            return Ok(employee);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAll()
        {
            var employees = await employeeService.GetAllAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> Create(EmployeeCreateDto employeeCreateDto)
        {
            await employeeService.CreateAsync(employeeCreateDto); ;

            return Ok(employeeCreateDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id , EmployeeUpdateDto employeeUpdateDto)
        {
            await employeeService.UpdateAsync(id, employeeUpdateDto);

            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            await employeeService.DeleteAsync(id);
            return Ok(true);
        }
    }
}
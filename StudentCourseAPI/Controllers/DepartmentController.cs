using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;
using StudentCourseAPI.Services;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentReadDto>>> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentReadDto>> GetId(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentReadDto>> Create(DepartmentCreateDto departmentCreateDto)
        {
            var department = await _departmentService.CreateAsync(departmentCreateDto);
            return Ok(department);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id , DepartmentUpdateDto departmentUpdateDto)
        {
            await _departmentService.UpdateAsync(id, departmentUpdateDto);

            return Ok(true);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> Delete(int id)
        {
            await _departmentService.DeleteAsync(id);
            return Ok(true);
        }

    }
}        
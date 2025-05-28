using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Services;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
            
        private readonly IDepartmentService _departmentService ;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentReadDto>>> GetAll()
        {
            var products = _departmentService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentReadDto>>GetId(int id)
        {
            var product = _departmentService.GetByIdAsync(id);

            return Ok(product);
        }
    }
}
        
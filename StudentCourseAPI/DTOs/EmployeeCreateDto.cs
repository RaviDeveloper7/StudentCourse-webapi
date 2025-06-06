using StudentCourseAPI.Models;

namespace StudentCourseAPI.DTOs
{
    public class EmployeeCreateDto
    {
        public string Name { get; set; }
        public EmployeeDetailCreateDto? EmployeeDetail { get; set; }
    }
}

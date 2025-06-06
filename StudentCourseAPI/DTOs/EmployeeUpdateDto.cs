using StudentCourseAPI.Models;

namespace StudentCourseAPI.DTOs
{
    public class EmployeeUpdateDto
    {
        public string Name { get; set; }
        public EmployeeDetailUpdateDto? EmployeeDetail { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace StudentCourseAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public EmployeeDetail? EmployeeDetail { get; set; }
    }
}
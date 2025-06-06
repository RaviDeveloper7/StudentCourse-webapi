using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCourseAPI.Models
{
    public class EmployeeDetail
    {

        public int Id { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }
            
        // Navigation property
        public Employee Employee { get; set; }

    }
}
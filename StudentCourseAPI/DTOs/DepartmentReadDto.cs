using System.ComponentModel.DataAnnotations;

namespace StudentCourseAPI.DTOs
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

    }
}
        
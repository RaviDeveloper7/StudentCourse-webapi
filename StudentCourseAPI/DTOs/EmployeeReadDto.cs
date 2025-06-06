    namespace StudentCourseAPI.DTOs
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployeeDetailReadDto? EmployeeDetail { get; set; }
    }
}
namespace StudentCourseAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Foreign key
        public int EmployeeId { get; set; }

        // Navigation property
        public Employee Employee { get; set; }

    }
}

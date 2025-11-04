namespace StudentCourseAPI.Models
{
    public class User
    {
        public int Id { get; set; }             // DB primary key
        public string UserName { get; set; }   // unique
        public string Email { get; set; }
        public string PasswordHash { get; set; } // store salted hash
        public string Role { get; set; } = "User"; // simple role

    }
}

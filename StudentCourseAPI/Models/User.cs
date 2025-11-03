namespace StudentCourseAPI.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}

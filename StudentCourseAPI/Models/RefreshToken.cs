namespace StudentCourseAPI.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }

        // relationship
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

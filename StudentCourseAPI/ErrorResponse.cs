using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace StudentCourseAPI
{
    public class ErrorResponse
    {
        public int statusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StrackTrace { get; set; }
    }
}

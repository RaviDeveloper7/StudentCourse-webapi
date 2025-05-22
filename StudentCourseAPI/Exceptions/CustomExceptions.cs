namespace StudentCourseAPI.Exceptions
{
    public class CustomExceptions
    {

    }

    // Exceptions/NotFoundException.cs
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    // Exceptions/BadRequestException.cs
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    // Exceptions/UnauthorizedException.cs
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }

}

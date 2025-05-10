namespace StudentCourseAPI
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Before next()");
            await _next(context);
            Console.WriteLine("After next()");

            Console.WriteLine($"Response status code: {context.Response.StatusCode}");

        }

    }

}

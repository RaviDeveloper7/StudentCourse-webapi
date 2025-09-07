using System.Diagnostics;

namespace StudentCourseAPI
{
    public class ElapsedTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ElapsedTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                context.Response.Headers.Append("X-Processing-Time-Ms", stopwatch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
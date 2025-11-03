using StudentCourseAPI.Exceptions;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace StudentCourseAPI
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue request pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"🔥 Exception: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";
            string? stackTrace = null;

            // Determine exception type
            switch (exception)
            {
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;

                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;

                default:
                    message = exception.Message;
                    break;
            }

            // Only show stack trace in development
            if (_env.IsDevelopment())
            {
                stackTrace = exception.StackTrace;
            }

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponse
            {
                statusCode = (int)statusCode,
                ErrorMessage = message,
                StrackTrace = stackTrace
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

    public static class  MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
          return  app.UseMiddleware<ExceptionMiddleware>();
        }

    }
}
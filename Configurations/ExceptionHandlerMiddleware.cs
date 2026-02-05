using System.Text.Json;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs;

namespace InMemoryDBSpecificationRepositoryUOWProject.Configurations
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = 0;
            statusCode = ex switch
            {
                Exceptions.BadRequestException => StatusCodes.Status400BadRequest,
                Exceptions.UnauthorizedException => StatusCodes.Status401Unauthorized,
                Exceptions.ForbiddenException => StatusCodes.Status403Forbidden,
                Exceptions.NotFoundException => StatusCodes.Status404NotFound,
                Exceptions.KeyNotFoundException => StatusCodes.Status404NotFound,
                Exceptions.ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            var result = JsonSerializer.Serialize(new ApiResponse { StatusCode = statusCode, Message = ex.Message, Data = null }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}

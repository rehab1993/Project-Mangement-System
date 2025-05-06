using DotNetCore.CAP.Messages;
using Microsoft.AspNetCore.Http;
using Project_Mangement_System.Commons;
using Project_Mangement_System.Commons.Data;
using Project_Mangement_System.Commons.Views;
using System.Text.Json;

namespace Project_Mangement_System.MiddleWares
{
    public class GlobalExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleWare> _logger;

        public GlobalExceptionMiddleWare(RequestDelegate next, ILogger<GlobalExceptionMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue request pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        // Handles exceptions and returns a standardized error response.
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            //var errorResponse = new EndPointResponse<string>
            //{
            //    Data = null,
            //    Message = exception.Message,
            //    IsSuccess = false,
            //    StatusCode = ErrorCode.InternalServerError
            //};

            var errorResponse = EndPointResponse<string>.Failure(
          ErrorCode.InternalServerError,
          exception.Message // or sanitize in production
      );
            // Convert response object to JSON format
            string jsonResponse = JsonSerializer.Serialize(errorResponse);
            // Set HTTP status code to 500 (Internal Server Error)
            response.StatusCode = (int)ErrorCode.InternalServerError;
            // Write the JSON response to the HTTP response body
            return response.WriteAsync(jsonResponse);
        }
    }
}

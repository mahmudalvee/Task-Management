using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManagement.API.Middlewares
{

    public class ExceptionHandleMidlleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMidlleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Catch
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, "An unhandled exception occurred during request processing for: {Path}", context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "An internal server error has occurred!",
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
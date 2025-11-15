
namespace TaskManagement.API.Middlewares
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandleMiddleware> _logger;

        public ErrorHandleMiddleware(RequestDelegate next, ILogger<ErrorHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var logDetails = $"[{DateTime.Now}] {request.Method} {request.Path}";

            _logger.LogInformation(logDetails);

            if (request.ContentLength > 0)
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                string body = System.Text.Encoding.UTF8.GetString(buffer);
                _logger.LogInformation($"Request Body: {body}");
                request.Body.Position = 0;
            }

            await _next(httpContext); //next

            var response = httpContext.Response;
            _logger.LogInformation($"Response Status: {response.StatusCode}");
        }
    }
}

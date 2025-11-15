namespace TaskManagement.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath = "logs/api-logs.txt";

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var logDirectory = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory); // Create logs folder if it doesn't exist
            }

            var request = httpContext.Request;
            var logDetails = $"{DateTime.Now:yyyy-MM-dd HH:mm} | {request.Method} {request.Path}";

            await LogToFileAsync($"Request: {logDetails}");
            await _next(httpContext);   //nextt

            var response = httpContext.Response;
            await LogToFileAsync($"Response: {logDetails} | Status: {response.StatusCode}");
        }

        private async Task LogToFileAsync(string message)
        {
            try
            {
                using (var writer = new StreamWriter(_logFilePath, append: true))
                {
                    await writer.WriteLineAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging: {ex.Message}");
            }
        }
    }
}

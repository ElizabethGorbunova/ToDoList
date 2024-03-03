using ToDoWebApp.Exceptions;

namespace ToDoWebApp.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException badRequestExc)
            {
                _logger.LogError(badRequestExc, badRequestExc.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestExc.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occured");
                
            }
        }
    }
}

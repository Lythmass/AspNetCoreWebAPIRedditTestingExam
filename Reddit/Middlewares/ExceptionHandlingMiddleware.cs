using System.Net;

namespace Reddit.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate nextDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this._nextDelegate = nextDelegate;
            this._logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                
                await _nextDelegate(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            }
        }

    }
}

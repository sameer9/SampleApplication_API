using Microsoft.AspNetCore.Diagnostics;

namespace SampleApplication.API.MyExceptionHandler
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler)
        {
            _next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await _exceptionHandler.TryHandleAsync(httpContext, ex, CancellationToken.None);
            }
        }
    }

}

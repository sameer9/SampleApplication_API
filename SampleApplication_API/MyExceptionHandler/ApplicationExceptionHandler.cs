using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApplication.API.MyExceptionHandler
{
    public class ApplicationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ApplicationExceptionHandler> _logger;

        public ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Log the exception

            _logger.LogError(exception, "An unexpected error occurred");

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "An unexpected error occurred",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            });

            return true;
        }



 
    }
}

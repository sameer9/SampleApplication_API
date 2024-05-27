using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SampleApplication.Models.Response;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SampleApplication.API.MyExceptionHandler
{
    public class ApplicationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse
            {
                Status = false,
                Errors = new List<string> { exception.Message }
            };

            switch (exception)
            {
                case DivideByZeroException:
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Errors.Add("Division by zero error.");
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case NotImplementedException:
                    apiResponse.StatusCode = HttpStatusCode.NotImplemented;
                    apiResponse.Errors.Add("The requested method is not implemented.");
                    httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
                    break;

                case IndexOutOfRangeException:
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Errors.Add("Index out of range error.");
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                default:
                    apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponse.Errors.Add("An unexpected error occurred.");
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await httpContext.Response.WriteAsJsonAsync(apiResponse, cancellationToken);

            return true;
        }
    }

}

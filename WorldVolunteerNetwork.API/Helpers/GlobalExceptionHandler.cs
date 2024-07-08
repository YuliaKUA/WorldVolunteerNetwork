using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.API.Helpers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            string exceptionMessage;
            if (exception != null)
            {
                exceptionMessage = ((CSharpFunctionalExtensions.ResultFailureException<WorldVolunteerNetwork.Domain.Common.Error>)exception).Error.Message;
            }
            else
            {
                exceptionMessage = exception.Message;
            }

            //var exceptionMessage = exception.Message;

            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exceptionMessage, DateTime.UtcNow);

            var response = new Response()
            {
                Code = "value.invalid",
                Message = exceptionMessage
            };

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            // Return false to continue with the default behavior
            // - or - return true to signal that this exception is handled

            return true;
        }
    }

    public class Response
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}

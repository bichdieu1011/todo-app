using System.Net;
using TodoApp.Services.Models;

namespace TodoApp.WebApp.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var errorResult = new ActionResult
                {
                    Result = Services.Constant.Result.Error,
                    Messages = new List<string> { exception.Message }
                };

                logger.LogError(exception.ToString());

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(errorResult));
                }
                else
                {
                    logger.LogWarning("Can't write error response. Response has already started.");
                }
            }
        }
    }
}
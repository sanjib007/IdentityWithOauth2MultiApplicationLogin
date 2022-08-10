using System.Net;
using Newtonsoft.Json;
using Utility;

namespace Client1.Middleware
{
    public class ExceptionFormattingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionFormattingMiddleware> _logger;

        public ExceptionFormattingMiddleware(RequestDelegate next, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _env = env;
            _logger = loggerFactory
                .CreateLogger<ExceptionFormattingMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex, _env);
            }
        }

        private Task HandleException(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = new ApiError();
            error.statusCode = (int)code;

            if (_env.IsDevelopment())
            {
                error.message = exception.StackTrace;
                error.details = exception.Message;
            }
            else
            {
                //  ...in production 
                error.message = "The service is unavailable";
                error.details = "The server is under maintenance";
            }

            if (exception is GlobalApplicationException)
            {
                error.message = exception.StackTrace;
                error.details = exception.Message;

                error.statusCode = (int)HttpStatusCode.UnprocessableEntity;
                code = HttpStatusCode.UnprocessableEntity;
            }
            
            else if (exception is UnauthorizedAccessException)
            {
                error.statusCode = 401;
                error.message = "Unauthorized Access";
                error.details = exception.Message;
                code = HttpStatusCode.Unauthorized;
            }
            
            
            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static string DummyMessage()
        {
            return "Oops, Something wrong on our side, Please try again";
        }
    }
}

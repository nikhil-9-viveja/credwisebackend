using System.Net;
using System.Text.Json;
using CredWiseAdmin.Service.Exceptions;

namespace CredWiseAdmin.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                int statusCode = (int)HttpStatusCode.InternalServerError;
                object response;

                switch (ex)
                {
                    case NotFoundException notFoundEx:
                        statusCode = (int)HttpStatusCode.NotFound;
                        response = new { message = notFoundEx.Message };
                        break;
                    case BusinessException businessEx:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        response = new { message = businessEx.Message };
                        break;
                    default:
                        response = _env.IsDevelopment()
                            ? new { message = ex.Message, stackTrace = ex.StackTrace }
                            : new { message = "An internal server error occurred." };
                        break;
                }

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
} 
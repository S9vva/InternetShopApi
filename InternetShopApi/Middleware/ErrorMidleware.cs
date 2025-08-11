using System.Text.Json;

namespace InternetShopApi.Middleware
{
    public class ErrorMidleware
    {
        private readonly ILogger<ErrorMidleware> _logger;
        private readonly RequestDelegate _request;


        public ErrorMidleware(ILogger<ErrorMidleware> logger, RequestDelegate request)
        {
            _logger = logger;
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request(context);

                if (!context.Response.HasStarted)
                {


                    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            statusCode = 403,
                            message = "You do not have access to this operation."
                        }));
                    }
                }
            }
            catch(ArgumentNullException ex)
            {
                await HandleErrorAsync(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ArgumentException ex)
            {
                await HandleErrorAsync(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    statusCode = 500,
                    message = "A server error has occurred",
                    detail = ex.Message
                }));

            }


        }

        private static Task HandleErrorAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new { statusCode, message });
            return context.Response.WriteAsync(result);
        }


    }

    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ErrorMidleware>();
    }




}

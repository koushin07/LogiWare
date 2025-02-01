using System.Net;
using System.Text.Json;
using Logiware.Application.Exception;

namespace Logiware.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;


    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

        }
        catch (System.Exception ex)
        {
            _logger.LogInformation(ex.Message);
            context.Response.ContentType = "application/json";

            // Check for specific exceptions and set status codes accordingly



            context.Response.StatusCode = ex switch
            {
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizeException => (int)HttpStatusCode.Unauthorized,
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => 500
            };

            context.Response.ContentType = "application/json";
            var statusCode = ex switch
            {
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizeException => (int)HttpStatusCode.Unauthorized,
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => 500
            };

            var response = new ApiException(
                statusCode,
                statusCode == 500 ? "Internal Server Error" : ex.GetType().FullName,
                _env.IsDevelopment() ? ex.StackTrace?.ToString() : "Internal Server Error",
                context.TraceIdentifier
            );



            // Optionally add specific error messages to the Errors dictionary
            if (ex is BadRequestException badRequestEx)
            {
                response.AddError("Bad Request", new[] { badRequestEx.Message });
            }
            if (ex is NotFoundException notFoundEx)
            {
                response.AddError("Not Found", new[] { notFoundEx.Message });
            }
            if (ex is UnauthorizeException unauthorizeExceptionEx)
            {
                response.AddError("Unauthorized", new[] { unauthorizeExceptionEx.Message });
            }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var json = JsonSerializer.Serialize(response, options);

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(json);
        }

    }
}
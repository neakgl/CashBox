using System.Text.Json;

namespace CashBox.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500; // 500 Internal Server Error

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sistemde beklenmedik bir hata oluştu.",
                DetailedError = error.Message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
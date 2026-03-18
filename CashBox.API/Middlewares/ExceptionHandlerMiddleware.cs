using System.Text.Json;
using CashBox.Core.DTOs.ResponseDTOs; // Standart paketimizi içeri aldık!

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

            // Hataya göre status kodu belirliyoruz
            var statusCode = error switch
            {
                KeyNotFoundException => 404,
                ArgumentException or InvalidOperationException => 400,
                _ => 500
            };

            context.Response.StatusCode = statusCode;

            // İŞTE ŞOV BURADA: Kendi standart CustomResponseDto'muzun Fail metodunu çağırıyoruz!
            // T tipi için object veriyoruz çünkü hata durumunda data dönmüyoruz.
            var response = CustomResponseDto<object>.Fail(statusCode, error.Message);

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
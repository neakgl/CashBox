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

            // Hataya göre Status Code ve Mesaj belirleme
            var (statusCode, message) = error switch
            {
                KeyNotFoundException => (404, "İstenen kayıt bulunamadı."),

                ArgumentException or InvalidOperationException => (400, "Geçersiz işlem veya eksik parametre gönderildi."),

                _ => (500, "Sistemde beklenmedik bir hata oluştu.")
            };

            context.Response.StatusCode = statusCode;

            // Yanıt Paketini Hazırlama
            var response = new
            {
                StatusCode = statusCode,
                Message = message,
                DetailedError = error.Message // Projeyi canlıya alırken güvenlik için bu gizlenmeli
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
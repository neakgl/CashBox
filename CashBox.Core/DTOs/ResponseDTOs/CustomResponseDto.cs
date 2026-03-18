using System.Text.Json.Serialization;

namespace CashBox.Core.DTOs.ResponseDTOs;

public class CustomResponseDto<T>
{
    public T Data { get; set; }

    public int StatusCode { get; set; }

    public List<string> Errors { get; set; }

    // Başarılı
    public static CustomResponseDto<T> Success(int statusCode, T data)
    {
        return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
    }

    // Başarılı (Sadece durum kodu dönen)
    public static CustomResponseDto<T> Success(int statusCode)
    {
        return new CustomResponseDto<T> { StatusCode = statusCode };
    }

    // Başarısız
    public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
    {
        return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
    }

    // Başarısız (Tek bir hata mesajı)
    public static CustomResponseDto<T> Fail(int statusCode, string error)
    {
        return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
    }
}
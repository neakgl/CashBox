using System.Security.Claims;

namespace CashBox.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı.");

        return int.Parse(userIdClaim.Value);
    }
}
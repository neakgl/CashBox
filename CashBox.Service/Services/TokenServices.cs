using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashBox.Core.DTOs.UserDTOs;
using CashBox.Core.Entities;
using CashBox.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CashBox.Service.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDto CreateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var expirationInMinutes = Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"]);
        var expirationDate = DateTime.Now.AddMinutes(expirationInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: expirationDate,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return new TokenDto
        {
            AccessToken = tokenHandler.WriteToken(token),
            Expiration = expirationDate
        };
    }
}

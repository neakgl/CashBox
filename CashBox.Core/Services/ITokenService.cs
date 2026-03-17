using CashBox.Core.DTOs.UserDTOs;
using CashBox.Core.Entities;

namespace CashBox.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(User user);
}
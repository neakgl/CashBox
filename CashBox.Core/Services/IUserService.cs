using CashBox.Core.DTOs.UserDTOs;
using CashBox.Core.Entities;

namespace CashBox.Core.Services;

public interface IUserService : IGenericService<User>
{
    Task<UserDto> AddWithDtoAsync(UserCreateDto dto);
    Task<IEnumerable<UserDto>> GetAllWithDtoAsync();
    Task UpdateWithDtoAsync(UserUpdateDto dto);
}

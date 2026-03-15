using AutoMapper;
using CashBox.Core.DTOs.UserDTOs;
using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;
using BCrypt.Net;

namespace CashBox.Service.Services;

public class UserService : GenericService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        : base(repository, unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> AddWithDtoAsync(UserCreateDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        await AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllWithDtoAsync()
    {
        var users = await GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task UpdateWithDtoAsync(UserUpdateDto dto)
    {
        var user = await GetByIdAsync(dto.Id);
        if (user != null)
        {
            _mapper.Map(dto, user);
            Update(user);
        }
    }
}
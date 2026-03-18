using CashBox.Core.DTOs.UserDTOs;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBox.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("getAllUsers")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllWithDtoAsync();
        return Ok(result);
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> Create(UserCreateDto userDto)
    {
        var result = await _userService.AddWithDtoAsync(userDto);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("updateUser")]
    public async Task<IActionResult> Update(UserUpdateDto updateDto)
    {
        await _userService.UpdateWithDtoAsync(updateDto);
        return Ok("Kullanıcı başarıyla güncellendi.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("removeUser")]
    public async Task<IActionResult> Remove(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound("Kullanıcı bulunamadı.");

        _userService.Remove(user);
        return Ok("Kullanıcı silindi.");
    }
    [HttpPost("loginUser")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        var result = await _userService.LoginAsync(loginDto);

        return Ok(result);
    }
}
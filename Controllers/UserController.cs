using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService=userService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users=await _userService.GetAllAsync();
        return Ok(users);
    } 
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user=await _userService.GetByIdAsync(id);
        return Ok(user);
    }
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto dto)
    {
        var user=await _userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetUser),new {id=user.Id},user);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id,UserUpdateDto dto)
    {
        var user=await _userService.UpdateAsync(id,dto);
        return Ok(user);
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult<UserDto>> PatchUser(int id,UserPatchDto dto)
    {
        var user=await _userService.PatchAsync(id,dto);
        return Ok(user);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService=authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenResponseDto>> Register(RegisterDto dto)
    {
        var result=await _authService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(LoginDto dto)
    {
        var result=await _authService.LoginAsync(dto);
        return Ok(result);
    }


}
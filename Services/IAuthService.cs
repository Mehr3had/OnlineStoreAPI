using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface IAuthService
{
    Task<TokenResponseDto> RegisterAsync(RegisterDto dto);
    Task<TokenResponseDto> LoginAsync(LoginDto dto);
}
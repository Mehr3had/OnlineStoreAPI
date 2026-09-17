using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Services;
public interface IJwtService
{
    string GenerateToken(User user);
}
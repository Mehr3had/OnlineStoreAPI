using Microsoft.AspNetCore.Identity;
using OnlineStoreAPI.Models;
namespace OnlineStoreAPI.Services;
public class PasswordService
{
    private readonly PasswordHasher<User> _passwordHasher;
    public PasswordService()
    {
        _passwordHasher=new PasswordHasher<User>();
    }
    public string HashPassword(User user,string password)
    {
        return _passwordHasher.HashPassword(user,password);
    }
    public bool VerifyPassword(User user,string hashedPassword,string password)
    {
        var result=_passwordHasher.VerifyHashedPassword(user,hashedPassword,password);
        return result==PasswordVerificationResult.Success;
    }
}
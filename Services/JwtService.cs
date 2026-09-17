using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    {
        _configuration=configuration;
    }
    public string GenerateToken(User user)
    {
        var claims=new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()
            ),
            new Claim(
                ClaimTypes.Email,
                user.Email
            ),
            new Claim(
                ClaimTypes.Role,
                user.Role.Name
            )
        };
        var key=new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );
        var credentials=new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );
        var expiration=DateTime.UtcNow.AddMinutes(
            double.Parse(
                _configuration["Jwt:ExpiresInMinutes"]!
            )
        );
        var token=new JwtSecurityToken(
            issuer:_configuration["Jwt:Issuer"],
            audience:_configuration["Jwt:Audience"],
            claims:claims,
            expires:expiration,
            signingCredentials:credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
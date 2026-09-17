using Microsoft.AspNetCore.Identity;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Repositories;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Exceptions;

namespace OnlineStoreAPI.Services;
public class AuthService:IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;
    public AuthService(IUnitOfWork unitOfWork,IJwtService jwtService)
    {
        _unitOfWork=unitOfWork;
        _passwordHasher=new PasswordHasher<User>();
        _jwtService=jwtService;
    }
    public async Task<TokenResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser=await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            throw new ConflictException("Email is already registered.");
        }
        var user=new User
        {
            FirstName=dto.FirstName,
            LastName=dto.LastName,
            Email=dto.Email,
            RoleId=2
        };
        user.Password=_passwordHasher.HashPassword(user,dto.Password);
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        var savedUser=await _unitOfWork.Users.GetByEmailAsync(user.Email);
        if (savedUser == null)
        {
            throw new NotFoundException("User not found.");
        }
        var token=_jwtService.GenerateToken(savedUser);
        return new TokenResponseDto
        {
            Token=token
        };
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user=await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        var result=_passwordHasher.VerifyHashedPassword(user,user.Password,dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new NotFoundException("Invalid email or password.");
        }
        var token=_jwtService.GenerateToken(user);
        return new TokenResponseDto
        {
            Token=token
        };
    }
}
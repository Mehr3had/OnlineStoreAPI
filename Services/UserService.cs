using OnlineStoreAPI.Repositories;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using OnlineStoreAPI.Exceptions;

namespace OnlineStoreAPI.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<UserDto> CreateAsync(UserCreateDto dto)
    {
        var user=new User
        {
            FirstName=dto.FirstName,
            LastName=dto.LastName,
            Email=dto.Email,
            Password=dto.Password,
            RoleId=dto.RoleId
        };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user=await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        await _unitOfWork.Users.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users=await _unitOfWork.Users.GetAllAsync();
        return users.Select(MapToDto).ToList();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user=await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        return MapToDto(user);
    }

    public async Task<UserDto> PatchAsync(int id, UserPatchDto dto)
    {
        var user=await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        if (dto.FirstName != null)
        {
            user.FirstName=dto.FirstName;
        }
        if (dto.LastName != null)
        {
            user.LastName=dto.LastName;
        }
        if (dto.Email != null)
        {
            user.Email=dto.Email;
        }
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(user);
    }

    public async Task<UserDto> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user=await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        user.FirstName=dto.FirstName;
        user.LastName=dto.LastName;
        user.Email=dto.Email;
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(user);
    }
    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id=user.Id,
            FirstName=user.FirstName,
            LastName=user.LastName,
            Email=user.Email
        };
    }
}
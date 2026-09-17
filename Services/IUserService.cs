using OnlineStoreAPI.DTOs;
public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(UserCreateDto dto);
    Task<UserDto> UpdateAsync(int id,UserUpdateDto dto);
    Task<UserDto> PatchAsync(int id,UserPatchDto dto);
    Task DeleteAsync(int id);
}
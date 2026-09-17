using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(int id);
    Task<CategoryDetailsDto> GetDetailsByIdAsync(int id); 
    Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
    Task<CategoryDto> UpdateAsync(int id,CategoryUpdateDto dto);
    Task DeleteAsync(int id);
    Task<CategoryDto> PatchAsync(int id,CategoryPatchDto dto); 
}
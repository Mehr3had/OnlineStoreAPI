using OnlineStoreAPI.DTOs;
namespace OnlineStoreAPI.Services;
public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductCreateDto dto);
    Task<ProductDto> UpdateAsync(int id,ProductUpdateDto dto);
    Task<ProductDto> PatchAsync(int id,ProductPatchDto dto);
    Task DeleteAsync(int id);
}
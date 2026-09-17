using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface IProductImageService
{
    Task<ProductImageDto> GetByIdAsync(int id);
    Task<List<ProductImageDto>> GetByProductIdAsync(int productId);
    Task<ProductImageDto> CreateAsync(ProductImageCreateDto dto);
    Task DeleteAsync(int id);
}
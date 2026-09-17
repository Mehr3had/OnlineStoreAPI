using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;
public interface IProductImageRepository
{
    Task<ProductImage?> GetByIdAsync(int id);
    Task<List<ProductImage>> GetByProductIdAsync(int productId);
    Task AddAsync(ProductImage productImage);
    Task DeleteAsync(ProductImage productImage);
}
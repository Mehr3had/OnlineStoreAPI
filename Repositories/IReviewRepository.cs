using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;
public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(int id);
    Task<List<Review>> GetByProductIdAsync(int productId); 
    Task<Review?> GetByUserAndProductAsync(int userId,int productId);
    Task AddAsync(Review review);
    Task DeleteAsync(Review review);
}
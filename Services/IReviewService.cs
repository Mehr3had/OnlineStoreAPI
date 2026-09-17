using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface IReviewService
{
    Task<ReviewDto> CreateAsync(ReviewCreateDto dto);
    Task<ReviewDto> GetByIdAsync(int id);
    Task<List<ReviewDto>> GetByProductIdAsync(int productId);
    Task DeleteAsync(int id);
}
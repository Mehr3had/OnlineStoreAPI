using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Services;
public interface ICartService
{
    Task<CartDto> GetByUserIdAsync(int userId); 
    Task<CartDto> AddItemAsync(int userId,CartItemCreateDto dto);
    Task<CartDto> UpdateItemAsync(int userId,int productId,CartItemUpdateDto dto);
    Task<CartDto> DeleteItemAsync(int userId,int productId);
    Task ClearItemsAsync(int userId);
}
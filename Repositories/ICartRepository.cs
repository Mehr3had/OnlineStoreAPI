using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Models;
namespace OnlineStoreAPI.Repositories;
public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task<CartItem?> GetItemAsync(int cartId,int productId);
    Task AddItemAsync(CartItem item);
    Task UpdateItemAsync(CartItem item);
    Task DeleteItemAsync(CartItem item);
    Task ClearItemsAsync(Cart cart);
}
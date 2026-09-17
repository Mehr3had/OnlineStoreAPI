using OnlineStoreAPI.Models;
using OnlineStoreAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace OnlineStoreAPI.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;
    public CartRepository(ApplicationDbContext context)
    {
        _context=context;
    }

    public async Task AddItemAsync(CartItem item)
    {
        await _context.CartItems.AddAsync(item);
    }

    public async Task ClearItemsAsync(Cart cart)
    {
        _context.CartItems.RemoveRange(cart.Items);
        await Task.CompletedTask;

    }

    public async Task DeleteItemAsync(CartItem item)
    {
        _context.CartItems.Remove(item);
        await Task.CompletedTask;
    }

    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        return await _context.Carts.Include(c=>c.Items).ThenInclude(ci=>ci.Product).
        FirstOrDefaultAsync(c=>c.UserId==userId);
    }

    public async Task<CartItem?> GetItemAsync(int cartId, int productId)
    {
        return await _context.CartItems.FirstOrDefaultAsync(ci=>ci.CartId==cartId && ci.ProductId==productId);
    }

    public async Task UpdateItemAsync(CartItem item)
    {
        _context.CartItems.Update(item);
        await Task.CompletedTask;
    }
}
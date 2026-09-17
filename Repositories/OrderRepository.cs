using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<List<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders.Include(o=>o.Items).ThenInclude(oi=>oi.Product)
        .Where(o=>o.UserId==userId).ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int Id)
    {
        return await _context.Orders.Include(o=>o.Items).ThenInclude(oi=>oi.Product)
        .FirstOrDefaultAsync(o=>o.Id==Id);
    }
}
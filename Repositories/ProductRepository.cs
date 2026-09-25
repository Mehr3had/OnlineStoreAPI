using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;
namespace OnlineStoreAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await Task.CompletedTask;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p=>p.Category).Include(p=>p.Images).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p=>p.Category).Include(p=>p.Images)
        .FirstOrDefaultAsync(p=>p.Id==id);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await Task.CompletedTask;
    }
}
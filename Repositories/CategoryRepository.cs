using OnlineStoreAPI.Models;
using OnlineStoreAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace OnlineStoreAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await Task.CompletedTask;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task<Category?> GetDetailsByIdAsync(int id)
    {
        return await _context.Categories.Include(c=>c.Products).FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task<bool> HasProductsAsync(int categoryId)
    {
        return await _context.Products.AnyAsync(p=>p.CategoryId==categoryId);
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await Task.CompletedTask;
    }
}
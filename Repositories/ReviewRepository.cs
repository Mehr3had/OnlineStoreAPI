using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;
    public ReviewRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
    }

    public async Task DeleteAsync(Review review)
    {
        _context.Reviews.Remove(review);
        await Task.CompletedTask;
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews.Include(r=>r.User)
        .Include(r=>r.Product).FirstOrDefaultAsync(r=>r.Id==id);
    }

    public async Task<List<Review>> GetByProductIdAsync(int productId)
    {
        return await _context.Reviews.Include(r=>r.User)
        .Include(r=>r.Product).Where(r=>r.ProductId==productId)
        .ToListAsync();
    }

    public async Task<Review?> GetByUserAndProductAsync(int userId, int productId)
    {
        return await _context.Reviews.Include(r=>r.User)
        .Include(r=>r.Product).FirstOrDefaultAsync(r=>r.UserId==userId && r.ProductId==productId);
    }
}
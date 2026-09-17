using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories;

public class ProductImageRepository : IProductImageRepository
{
    private readonly ApplicationDbContext _context;
    public ProductImageRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public async Task<ProductImage?> GetByIdAsync(int id)
    {
        return await _context.ProductImages.Include(pi=>pi.Product)
        .FirstOrDefaultAsync(pi=>pi.Id==id);
    }

    public async Task<List<ProductImage>> GetByProductIdAsync(int productId)
    {
        return await _context.ProductImages.Include(pi=>pi.Product)
        .Where(pi=>pi.ProductId==productId).ToListAsync();
    }

    public async Task AddAsync(ProductImage productImage)
    {
        await _context.ProductImages.AddAsync(productImage);
    }

    public async Task DeleteAsync(ProductImage productImage)
    {
        _context.ProductImages.Remove(productImage);
        await Task.CompletedTask;
    }

}
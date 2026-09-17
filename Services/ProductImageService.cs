using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Exceptions;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services;
public class ProductImageService:IProductImageService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductImageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<ProductImageDto> GetByIdAsync(int id)
    {
        var image=await _unitOfWork.Images.GetByIdAsync(id);
        if (image == null)
        {
            throw new NotFoundException("Product image not found.");
        }
        return MapToDto(image);
    }

    public async Task<List<ProductImageDto>> GetByProductIdAsync(int productId)
    {
        var images=await _unitOfWork.Images.GetByProductIdAsync(productId);
        return images.Select(MapToDto).ToList();
    }

    public async Task<ProductImageDto> CreateAsync(ProductImageCreateDto dto)
    {
        var product=await _unitOfWork.Products.GetByIdAsync(dto.ProductId);
        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }
        var image=new ProductImage
        {
            ProductId=dto.ProductId,
            ImageUrl=dto.ImageUrl
        };
        await _unitOfWork.Images.AddAsync(image);
        await _unitOfWork.SaveChangesAsync();
        return await GetByIdAsync(image.Id)
        ??throw new NotFoundException("Product image not found.");
    }

    public async Task DeleteAsync(int id)
    {
        var image=await _unitOfWork.Images.GetByIdAsync(id);
        if (image == null)
        {
            throw new NotFoundException("Product image not found.");
        }
        await _unitOfWork.Images.DeleteAsync(image);
        await _unitOfWork.SaveChangesAsync();
    }
    
    private static ProductImageDto MapToDto(ProductImage image)
    {
        return new ProductImageDto
        {
            Id=image.Id,
            ProductId=image.ProductId,
            ProductTitle=image.Product.Title,
            ImageUrl=image.ImageUrl
        };
    }
}
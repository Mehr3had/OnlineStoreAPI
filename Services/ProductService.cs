using OnlineStoreAPI.Repositories;
using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using OnlineStoreAPI.Exceptions;

namespace OnlineStoreAPI.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }

    public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
    {
        var category=await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        var product=new Product
        {
            Title=dto.Title,
            Description=dto.Description,
            Price=dto.Price,
            CategoryId=dto.CategoryId
        };
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(product);
    }    

    public async Task DeleteAsync(int id)
    {
        var product=await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }
        await _unitOfWork.Products.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products=await _unitOfWork.Products.GetAllAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product=await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }
        return MapToDto(product);
    }

    public async Task<ProductDto> PatchAsync(int id, ProductPatchDto dto)
    {
        var product=await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }
        if (dto.Title != null)
        {
            product.Title=dto.Title;
        }
        if (dto.Description != null)
        {
            product.Description=dto.Description;
        }
        if (dto.Price.HasValue)
        {
            product.Price=dto.Price.Value;
        }
        if (dto.CategoryId.HasValue)
        {
            var category=await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId.Value);
            if (category == null)
            {
                throw new NotFoundException("Category not found.");
            }
            product.CategoryId=dto.CategoryId.Value;
        }
        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(product);
    }

    public async Task<ProductDto> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product=await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }
        var category=await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        product.Title=dto.Title;
        product.Description=dto.Description;
        product.Price=dto.Price;
        product.CategoryId=dto.CategoryId;
        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(product);
    }
    
    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id=product.Id,
            Title=product.Title,
            Description=product.Description,
            Price=product.Price,
            CategoryId=product.CategoryId,
            Category=product.Category==null
            ?null
            :new ProductCategoryDto
            {
                Id=product.Category.Id,
                Name=product.Category.Name
            }
        };
    }
}
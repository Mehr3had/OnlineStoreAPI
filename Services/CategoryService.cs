using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Exceptions;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;
namespace OnlineStoreAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
    {
        Category category=new Category
        {
            Name=dto.Name,
            Description=dto.Description
        };
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category=await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        var hasProducts=await _unitOfWork.Categories.HasProductsAsync(id);
        if (!hasProducts)
        {
            throw new ConflictException("Category cannot be deleted it has products.");
        }
        await _unitOfWork.Categories.DeleteAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories=await _unitOfWork.Categories.GetAllAsync();
        return categories.Select(MapToDto).ToList();
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category=await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        return MapToDto(category);
    }

    public async Task<CategoryDetailsDto> GetDetailsByIdAsync(int id)
    {
        var category=await _unitOfWork.Categories.GetDetailsByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        return new CategoryDetailsDto
        {
            Id=category.Id,
            Name=category.Name,
            Description=category.Description,
            Products=category.Products.Select(p=>new CategoryProductDto
            {
                Id=p.Id,
                Title=p.Title,
                Price=p.Price
            }).ToList()
        };
    }

    public async Task<CategoryDto> PatchAsync(int id, CategoryPatchDto dto)
    {
        var category=await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        if (dto.Name != null)
        {
            category.Name=dto.Name;
        }
        if (dto.Description != null)
        {
            category.Description=dto.Description;
        }
        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(category);
    }

    public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var category=await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }
        category.Name=dto.Name;
        category.Description=dto.Description;
        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(category);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id=category.Id,
            Name=category.Name,
            Description=category.Description
        };
    }
}

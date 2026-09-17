using OnlineStoreAPI.DTOs;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;
using OnlineStoreAPI.Exceptions;

namespace OnlineStoreAPI.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<ReviewDto> CreateAsync(ReviewCreateDto dto)
    {
        var existingReview=await _unitOfWork.Reviews.GetByUserAndProductAsync(dto.UserId,dto.ProductId);
        if (existingReview != null)
        {
            throw new ConflictException("User has already reviewed this product.");
        }
        var review=new Review
        {
            UserId=dto.UserId,
            ProductId=dto.ProductId,
            Rating=dto.Rating,
            Comment=dto.Comment,
            CreatedAt=DateTime.Now
        };
        await _unitOfWork.Reviews.AddAsync(review);
        await _unitOfWork.SaveChangesAsync();
        return await GetByIdAsync(review.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var review=await _unitOfWork.Reviews.GetByIdAsync(id);
        if (review == null)
        {
            throw new NotFoundException("Review not found.");
        }
        await _unitOfWork.Reviews.DeleteAsync(review);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ReviewDto> GetByIdAsync(int id)
    {
        var review=await _unitOfWork.Reviews.GetByIdAsync(id);
        if (review == null)
        {
            throw new NotFoundException("Review not found.");
        }
        return MapToDto(review);
    }

    public async Task<List<ReviewDto>> GetByProductIdAsync(int productId)
    {
        var reviews=await _unitOfWork.Reviews.GetByProductIdAsync(productId);
        return reviews.Select(MapToDto).ToList();
    }
    
    private static ReviewDto MapToDto(Review review)
    {
        return new ReviewDto
        {
            Id=review.Id,
            UserId=review.UserId,
            UserName=$"{review.User.FirstName} {review.User.LastName}",
            ProductId=review.ProductId,
            ProductTitle=review.Product.Title,
            Rating=review.Rating,
            Comment=review.Comment,
            CreatedAt=review.CreatedAt
        };
    }
}
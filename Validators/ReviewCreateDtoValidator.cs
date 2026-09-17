using System.Data;
using FluentValidation;
using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Validators;
public class ReviewCreateDtoValidator:AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {
        RuleFor(x=>x.UserId)
        .GreaterThan(0)
        .WithMessage("UserId must be greater than 0.");

        RuleFor(x=>x.ProductId)
        .GreaterThan(0)
        .WithMessage("ProductId must be greater than 0.");

        RuleFor(x=>x.Rating)
        .InclusiveBetween(1,5)
        .WithMessage("Rating must be between 1 and 5.");

        RuleFor(x=>x.Comment)
        .NotEmpty()
        .WithMessage("Comment is required.")
        .MaximumLength(500)
        .WithMessage("Comment cannot exceed 500 characters.");
    }
}
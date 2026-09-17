using FluentValidation;
using OnlineStoreAPI.DTOs;

namespace OnlineStoreAPI.Validators;
public class ProductPatchDtoValidator : AbstractValidator<ProductPatchDto>
{
    public ProductPatchDtoValidator()
    {
        RuleFor(x=>x.Title)
        .NotEmpty()
        .When(x=>x.Title!=null)
        .WithMessage("Title cannot be empty.");

        RuleFor(x=>x.Title)
        .MaximumLength(200)
        .When(x=>x.Title!=null)
        .WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x=>x.Price)
        .GreaterThan(0)
        .When(x=>x.Price.HasValue)
        .WithMessage("Price must be greater than 0.");

        RuleFor(x=>x.CategoryId)
        .GreaterThan(0)
        .When(x=>x.CategoryId.HasValue)
        .WithMessage("CategoryId must be greater than 0.");
    }
}
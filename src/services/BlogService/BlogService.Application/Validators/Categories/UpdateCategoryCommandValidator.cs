using BlogService.Application.DTOs.Categories;
using FluentValidation;

namespace BlogService.Application.Validators.Categories;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryCommandValidator()
    {
        
    }
}
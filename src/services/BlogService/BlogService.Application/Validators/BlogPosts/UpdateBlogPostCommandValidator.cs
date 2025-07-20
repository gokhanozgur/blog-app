using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Features.BlogPosts.Commands;
using FluentValidation;

namespace BlogService.Application.Validators.BlogPosts;

public class UpdateBlogPostCommandValidator :  AbstractValidator<UpdateBlogPostDto>
{
    public UpdateBlogPostCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id  cannot be empty.");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Property cannot be empty.")
            .MaximumLength(200).WithMessage("Property cannot be longer than 200 characters.")
            .MinimumLength(10).WithMessage("Property cannot be less than 10 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Property cannot be empty.");
        
        RuleFor(x => x.IsActive)
            .Must(x => x == false || x == true).WithMessage("Property should be boolean.");
    }
}
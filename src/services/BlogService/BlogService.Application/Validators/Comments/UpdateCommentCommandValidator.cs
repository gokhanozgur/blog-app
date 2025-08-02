using BlogService.Application.DTOs.Comments;
using BlogService.Application.Features.Comments.Commands;
using FluentValidation;

namespace BlogService.Application.Validators.Comments;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentDto>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty();
    }
}
using BlogService.Application.DTOs.Comments;
using BlogService.Application.Features.Comments.Commands;
using FluentValidation;

namespace BlogService.Application.Validators.Comments;

public class CreateCommentCommandValidator  : AbstractValidator<CreateCommentDto>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty();
    }
}
using MediatR;

namespace BlogService.Application.Features.BlogPosts.Commands;

public class DeleteBlogPostCommand : IRequest<bool>
{
    public string Id { get; set; }
}
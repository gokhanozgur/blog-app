using BlogService.Application.DTOs.BlogPosts;
using MediatR;

namespace BlogService.Application.Features.BlogPosts.Queries;

public class GetBlogPostByIdQuery : IRequest<BlogPostDto>
{
    public string Id { get; set; }
}
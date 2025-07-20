using BlogService.Application.DTOs.BlogPosts;
using MediatR;

namespace BlogService.Application.Features.BlogPosts.Queries;

public class GetAllBlogPostsQuery : IRequest<List<BlogPostDto>>
{
    
}
using BlogService.Application.DTOs.BlogPosts;
using MediatR;

namespace BlogService.Application.Features.BlogPosts.Commands;

public class UpdateBlogPostCommand : IRequest<BlogPostDto>
{
    public string BlogPostId { get; set; }
    public UpdateBlogPostDto UpdateBlogPostDto  { get; set; }

    public UpdateBlogPostCommand(string id, UpdateBlogPostDto dto)
    {
        BlogPostId = id;
        UpdateBlogPostDto = dto;
    }
}
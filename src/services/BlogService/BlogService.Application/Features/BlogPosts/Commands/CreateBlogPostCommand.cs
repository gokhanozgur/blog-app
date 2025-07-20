using BlogService.Application.DTOs.BlogPosts;
using MediatR;

namespace BlogService.Application.Features.BlogPosts.Commands;

public class CreateBlogPostCommand : IRequest<BlogPostDto>
{
    public CreateBlogPostDto CreateBlogPostDto { get; set; }
    public CreateBlogPostCommand(CreateBlogPostDto dto) => CreateBlogPostDto = dto;
}
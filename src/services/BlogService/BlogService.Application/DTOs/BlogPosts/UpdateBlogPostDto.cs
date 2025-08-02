using BlogService.Application.DTOs.Categories;
using BlogService.Domain.Entities;

namespace BlogService.Application.DTOs.BlogPosts;

public class UpdateBlogPostDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsActive { get; set; }
    
    public List<string>? CategoryIds { get; set; }
}
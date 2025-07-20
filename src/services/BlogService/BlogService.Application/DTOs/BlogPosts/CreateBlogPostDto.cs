using BlogService.Domain.Entities;

namespace BlogService.Application.DTOs.BlogPosts;

public class CreateBlogPostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsActive { get; set; }
    
    public List<Category> Tags { get; set; }
}
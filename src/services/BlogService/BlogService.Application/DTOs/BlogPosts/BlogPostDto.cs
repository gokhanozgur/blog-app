using BlogService.Domain.Entities;

namespace BlogService.Application.DTOs.BlogPosts;

public class BlogPostDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<Category> Tags { get; set; }
}
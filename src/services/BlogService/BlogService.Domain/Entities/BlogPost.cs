using BlogService.Domain.Common;

namespace BlogService.Domain.Entities;

public class BlogPost : BaseEntity
{
    
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public bool IsActive { get; set; } = false;
    
    public List<Category> Tags { get; set; } = new();
}
using BlogService.Domain.Common;

namespace BlogService.Domain.Entities;

public class BlogPost : BaseEntity
{
    
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public bool IsActive { get; set; } = false;
    
    public List<string> CategoryIds { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}
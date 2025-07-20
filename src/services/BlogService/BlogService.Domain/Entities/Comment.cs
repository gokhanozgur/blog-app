using BlogService.Domain.Common;

namespace BlogService.Domain.Entities;

public class Comment : BaseEntity
{
    public string BlogPostId { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string Content { get; set; } = null!;
}
namespace BlogService.Application.DTOs.Comments;

public class CommentDto
{
    public string Id { get; set; }
    public string BlogPostId { get; set; }
    public string AuthorId { get; set; }
    public string Content { get; set; }
    public bool IsValidated { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
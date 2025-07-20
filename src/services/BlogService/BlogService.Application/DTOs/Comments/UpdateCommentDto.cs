namespace BlogService.Application.DTOs.Comments;

public class UpdateCommentDto
{
    public string Id { get; set; }
    public string BlogPostId { get; set; }
    public string Content { get; set; }
    public bool IsValidated { get; set; }
}
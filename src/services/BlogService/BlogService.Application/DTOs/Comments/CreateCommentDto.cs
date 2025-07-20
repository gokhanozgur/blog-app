namespace BlogService.Application.DTOs.Comments;

public class CreateCommentDto
{
    public string BlogPostId { get; set; }
    public string AuthorId { get; set; }
    public string Content { get; set; }
    public bool IsValidated { get; set; }
}
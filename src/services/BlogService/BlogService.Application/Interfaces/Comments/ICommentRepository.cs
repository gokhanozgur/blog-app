using BlogService.Domain.Entities;

namespace BlogService.Application.Interfaces.Comments;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment post);
    Task<Comment?> GetByIdAsync(string id);
    Task<List<Comment>> GetByBlogPostIdAsync(string blogPostId);
    Task<List<Comment>> GetAllAsync();
    Task<Comment> UpdateAsync(Comment post);
    Task<bool> DeleteAsync(string id);
}
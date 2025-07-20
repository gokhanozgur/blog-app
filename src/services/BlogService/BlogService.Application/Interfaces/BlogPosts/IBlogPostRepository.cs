using BlogService.Domain.Entities;

namespace BlogService.Application.Interfaces.BlogPosts;

public interface IBlogPostRepository
{
    Task<BlogPost> CreateAsync(BlogPost post);
    Task<BlogPost?> GetByIdAsync(string id);
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost> UpdateAsync(BlogPost post);
    Task<bool> DeleteAsync(string id);
}
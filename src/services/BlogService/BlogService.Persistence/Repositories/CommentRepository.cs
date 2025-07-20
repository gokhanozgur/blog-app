using BlogService.Application.Interfaces.Comments;
using BlogService.Domain.Entities;
using BlogService.Persistence.Contexts;
using MongoDB.Driver;

namespace BlogService.Persistence.Repositories;

public class CommentRepository :  ICommentRepository
{
    private readonly IMongoCollection<Comment> _collection;

    public CommentRepository(MongoDbContext context)
    {
        _collection = context.Comments;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _collection.InsertOneAsync(comment);
        return comment;
    }

    public async Task<Comment?> GetByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Comment>> GetByBlogPostIdAsync(string blogPostId)
    {
        return await _collection.Find(x => x.BlogPostId == blogPostId).ToListAsync();
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        await _collection.ReplaceOneAsync(x => x.Id == comment.Id, comment);
        return comment;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
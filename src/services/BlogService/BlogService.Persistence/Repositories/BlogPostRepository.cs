using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Domain.Entities;
using BlogService.Persistence.Contexts;
using MongoDB.Driver;

namespace BlogService.Persistence.Repositories;

public class BlogPostRepository :  IBlogPostRepository
{
    private readonly IMongoCollection<BlogPost> _collection;

    public BlogPostRepository(MongoDbContext context)
    {
        _collection = context.BlogPosts;
    }

    public async Task<BlogPost> CreateAsync(BlogPost post)
    {
        await _collection.InsertOneAsync(post);
        return post;
    }

    public async Task<BlogPost?> GetByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<BlogPost>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<BlogPost> UpdateAsync(BlogPost post)
    {
        await _collection.ReplaceOneAsync(x => x.Id == post.Id, post);
        return post;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
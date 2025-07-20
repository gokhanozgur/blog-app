using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using BlogService.Persistence.Contexts;
using MongoDB.Driver;

namespace BlogService.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _collection;

    public CategoryRepository(MongoDbContext context)
    {
        _collection = context.Category;
    }

    public async Task<Category> CreateAsync(Category post)
    {
        await _collection.InsertOneAsync(post);
        return post;
    }

    public async Task<Category?> GetByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Category> UpdateAsync(Category post)
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
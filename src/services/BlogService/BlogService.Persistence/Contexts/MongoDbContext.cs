using BlogService.Domain.Entities;
using BlogService.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlogService.Persistence.Contexts;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly string _blogPostsCollectionName = "BlogPosts";
    private readonly string _commentsCollectionName = "Comments";
    private readonly string _categoriesCollectionName = "Categories";

    public MongoDbContext(IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<BlogPost> BlogPosts => _database.GetCollection<BlogPost>(_blogPostsCollectionName);
    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>(_commentsCollectionName);
    public IMongoCollection<Category> Categories => _database.GetCollection<Category>(_categoriesCollectionName);
}
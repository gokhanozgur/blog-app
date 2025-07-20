namespace BlogService.Persistence.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string BlogPostsCollectionName { get; set; } = "BlogPosts";
}
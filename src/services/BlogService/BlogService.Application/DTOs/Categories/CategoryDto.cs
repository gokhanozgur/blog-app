namespace BlogService.Application.DTOs.Categories;

public class CategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
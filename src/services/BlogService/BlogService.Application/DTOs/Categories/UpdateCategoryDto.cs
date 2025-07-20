namespace BlogService.Application.DTOs.Categories;

public class UpdateCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
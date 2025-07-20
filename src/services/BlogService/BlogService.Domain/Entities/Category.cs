using BlogService.Domain.Common;

namespace BlogService.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}
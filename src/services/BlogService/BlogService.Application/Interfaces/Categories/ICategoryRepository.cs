using BlogService.Domain.Entities;

namespace BlogService.Application.Interfaces.Categories;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category post);
    Task<Category?> GetByIdAsync(string id);
    Task<List<Category>> GetAllAsync();
    Task<Category> UpdateAsync(Category post);
    Task<bool> DeleteAsync(string id);
}
using AutoMapper;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Interfaces.Categories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Categories.Handlers;

public class UpdateCategoryCommandHandler :  IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            throw new NotFoundException("Category not found");
        
        _mapper.Map(request.UpdateCategoryDto, category);
        
        category.UpdatedAt = DateTime.UtcNow;
        
        await _categoryRepository.UpdateAsync(category);
        _logger.LogInformation($"Category {category.Id} updated");
        
        return _mapper.Map<CategoryDto>(category);
    }
}
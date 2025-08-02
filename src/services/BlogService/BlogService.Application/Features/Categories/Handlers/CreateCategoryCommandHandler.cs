using AutoMapper;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Categories.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CreateCategoryDto);
        
        await _categoryRepository.CreateAsync(category);
        _logger.LogInformation($"Category {category.Id} has been created");
        
        return _mapper.Map<CategoryDto>(category);
    }
}
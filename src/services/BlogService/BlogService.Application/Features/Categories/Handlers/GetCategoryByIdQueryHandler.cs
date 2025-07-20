using AutoMapper;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.Categories.Queries;
using BlogService.Application.Interfaces.Categories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Categories.Handlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetCategoryByIdQueryHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            throw new NotFoundException($"Category {request.CategoryId} not found");
        
        _logger.LogInformation($"Getting category {request.CategoryId}");
        return _mapper.Map<CategoryDto>(category);
    }
}
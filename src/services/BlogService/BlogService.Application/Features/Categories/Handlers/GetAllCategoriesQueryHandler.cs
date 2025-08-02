using AutoMapper;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Features.Categories.Queries;
using BlogService.Application.Interfaces.Categories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Categories.Handlers;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCategoriesQueryHandler> _logger;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetAllCategoriesQueryHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories =  await _categoryRepository.GetAllAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}
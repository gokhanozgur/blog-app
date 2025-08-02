using AutoMapper;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Interfaces.Categories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Categories.Handlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<DeleteCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new NotFoundException($"Category {request.Id} not found");
        
        _logger.LogInformation($"Deleting category {request.Id}");
        return await _categoryRepository.DeleteAsync(request.Id);
    }
}
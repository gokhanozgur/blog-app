using AutoMapper;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedEvents.Events.Categories;

namespace BlogService.Application.Features.Categories.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository, 
        IPublishEndpoint publishEndpoint, 
        IMapper mapper, 
        ILogger<CreateCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CreateCategoryDto);
        
        await _categoryRepository.CreateAsync(category);

        var @event = new CategoryCreatedEvent
        {
            Id = category.Id,
            Name = category.Name,
        };
        
        await _publishEndpoint.Publish(@event, cancellationToken);
        
        _logger.LogInformation($"Category {category.Id} has been created");
        
        return _mapper.Map<CategoryDto>(category);
    }
}
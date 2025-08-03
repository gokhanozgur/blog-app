using AutoMapper;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.Categories.Commands;
using BlogService.Application.Interfaces.Categories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedEvents.Events.Categories;

namespace BlogService.Application.Features.Categories.Handlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(
        ICategoryRepository categoryRepository, 
        IPublishEndpoint publishEndpoint,
        IMapper mapper, 
        ILogger<DeleteCategoryCommandHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new NotFoundException($"Category {request.Id} not found");

        bool deleteStatus = await _categoryRepository.DeleteAsync(request.Id);
        
        
        if (deleteStatus)
        {
            var @event = new CategoryDeletedEvent()
            {
                Id = category.Id,
            };
        
            await _publishEndpoint.Publish(@event, cancellationToken);
            
            _logger.LogInformation($"Deleting category {request.Id}");
        }
        
        return deleteStatus;
    }
}
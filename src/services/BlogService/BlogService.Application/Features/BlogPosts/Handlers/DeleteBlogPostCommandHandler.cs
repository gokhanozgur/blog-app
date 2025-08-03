using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedEvents.Events.BlogPosts;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, bool>
{
    private readonly IBlogPostRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<DeleteBlogPostCommandHandler> _logger;

    public DeleteBlogPostCommandHandler(
        IBlogPostRepository repository,  
        IPublishEndpoint publishEndpoint,
        ILogger<DeleteBlogPostCommandHandler> logger)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.Id);
        if (post == null)
            throw new BlogPostNotFoundException("Blog post not found.");

        bool deleteStatus = await _repository.DeleteAsync(request.Id);

        if (deleteStatus)
        {
            var @event = new BlogPostDeletedEvent()
            {
                BlogPostId = post.Id,
            };
        
            await _publishEndpoint.Publish(@event, cancellationToken);
            
            _logger.LogInformation($"Blog post with id {post.Id} has been deleted.");
        }

        return deleteStatus;
    }
}
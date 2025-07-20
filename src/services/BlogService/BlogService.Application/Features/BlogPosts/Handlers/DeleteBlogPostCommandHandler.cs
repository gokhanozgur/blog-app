using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, bool>
{
    private readonly IBlogPostRepository _repository;
    private readonly ILogger<DeleteBlogPostCommandHandler> _logger;

    public DeleteBlogPostCommandHandler(IBlogPostRepository repository,  ILogger<DeleteBlogPostCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.Id);
        if (post == null)
            throw new BlogPostNotFoundException("Blog post not found.");
        
        _logger.LogInformation($"Blog post with id {post.Id} has been deleted.");
        return await _repository.DeleteAsync(request.Id);
    }
}
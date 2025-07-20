using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class UpdateBlogPostCommandHandler :  IRequestHandler<UpdateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBlogPostCommandHandler> _logger;

    public UpdateBlogPostCommandHandler(IBlogPostRepository repository, IMapper mapper,  ILogger<UpdateBlogPostCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.BlogPostId);
        if (post == null)
            throw new BlogPostNotFoundException("Blog post not found.");

        _mapper.Map(request.UpdateBlogPostDto, post);

        post.UpdatedAt = DateTime.UtcNow;
        
        await _repository.UpdateAsync(post);
        _logger.LogInformation($"User {post.Id} has been updated.");

        return _mapper.Map<BlogPostDto>(post);
    }
}
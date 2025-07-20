using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Queries;
using BlogService.Application.Interfaces.BlogPosts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    private readonly IBlogPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBlogPostByIdQueryHandler> _logger;

    public GetBlogPostByIdQueryHandler(IBlogPostRepository repository, IMapper mapper,  ILogger<GetBlogPostByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.Id);
        if (post == null)
            throw new BlogPostNotFoundException("Blog post not found.");

        _logger.LogInformation($"User {post.Id} has been retrieved.");
        return _mapper.Map<BlogPostDto>(post);
    }
}
using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;

    public CreateBlogPostCommandHandler(IBlogPostRepository repository, IMapper mapper, ILogger<CreateBlogPostCommandHandler>  logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<BlogPost>(request.CreateBlogPostDto);

        await _repository.CreateAsync(post);
        _logger.LogInformation("Blog post created.");

        return _mapper.Map<BlogPostDto>(post);
    }
}
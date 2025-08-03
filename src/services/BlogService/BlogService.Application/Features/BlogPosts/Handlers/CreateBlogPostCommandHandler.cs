using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedEvents.Events.BlogPosts;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;

    public CreateBlogPostCommandHandler(
        IBlogPostRepository blogPostRepository, 
        ICategoryRepository categoryRepository, 
        IPublishEndpoint publishEndpoint,
        IMapper mapper, ILogger<CreateBlogPostCommandHandler>  logger)
    {
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<BlogPost>(request.CreateBlogPostDto);

        await _blogPostRepository.CreateAsync(post);
        
        var @event = new BlogPostCreatedEvent
        {
            BlogPostId = post.Id,
            Title = post.Title,
            AuthorId = post.AuthorId,
            CreatedAt = post.CreatedAt
        };
        
        await _publishEndpoint.Publish(@event, cancellationToken);
        
        if (post.CategoryIds.Any())
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            post.Categories = allCategories
                .Where(c => post.CategoryIds.Contains(c.Id))
                .Select(_mapper.Map<Category>)
                .ToList();
        }
        
        _logger.LogInformation("Blog post created.");

        return _mapper.Map<BlogPostDto>(post);
    }
}
using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.DTOs.Categories;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;

    public CreateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository, IMapper mapper, ILogger<CreateBlogPostCommandHandler>  logger)
    {
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<BlogPost>(request.CreateBlogPostDto);

        await _blogPostRepository.CreateAsync(post);
        
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
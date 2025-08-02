using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Queries;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBlogPostByIdQueryHandler> _logger;

    public GetBlogPostByIdQueryHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository, IMapper mapper,  ILogger<GetBlogPostByIdQueryHandler> logger)
    {
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _blogPostRepository.GetByIdAsync(request.Id);
        if (post == null)
            throw new BlogPostNotFoundException("Blog post not found.");
        
        if (post.CategoryIds.Any())
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            post.Categories = allCategories
                .Where(c => post.CategoryIds.Contains(c.Id))
                .Select(_mapper.Map<Category>)
                .ToList();
        }

        _logger.LogInformation($"User {post.Id} has been retrieved.");
        return _mapper.Map<BlogPostDto>(post);
    }
}
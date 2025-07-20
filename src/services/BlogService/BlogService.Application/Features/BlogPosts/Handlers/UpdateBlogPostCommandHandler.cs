using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class UpdateBlogPostCommandHandler :  IRequestHandler<UpdateBlogPostCommand, BlogPostDto>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBlogPostCommandHandler> _logger;

    public UpdateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository, IMapper mapper,  ILogger<UpdateBlogPostCommandHandler> logger)
    {
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _blogPostRepository.GetByIdAsync(request.BlogPostId);
        if (post == null)
            throw new BlogPostNotFoundException("Blog post not found.");

        _mapper.Map(request.UpdateBlogPostDto, post);

        post.UpdatedAt = DateTime.UtcNow;
        
        await _blogPostRepository.UpdateAsync(post);
        
        if (post.CategoryIds.Any())
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            post.Categories = allCategories
                .Where(c => post.CategoryIds.Contains(c.Id))
                .Select(_mapper.Map<Category>)
                .ToList();
        }
        
        _logger.LogInformation($"Blog {post.Id} has been updated.");

        return _mapper.Map<BlogPostDto>(post);
    }
}
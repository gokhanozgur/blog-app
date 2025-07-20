using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Features.BlogPosts.Queries;
using BlogService.Application.Interfaces.BlogPosts;
using BlogService.Application.Interfaces.Categories;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class GetAllBlogPostsQueryHandler : IRequestHandler<GetAllBlogPostsQuery, List<BlogPostDto>>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;

    public GetAllBlogPostsQueryHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository, IMapper mapper,  ILogger<CreateBlogPostCommandHandler> logger)
    {
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<BlogPostDto>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _blogPostRepository.GetAllAsync();

        foreach (var post in posts)
        {
            if (post.CategoryIds.Any())
            {
                var allCategories = await _categoryRepository.GetAllAsync();
                post.Categories = allCategories
                    .Where(c => post.CategoryIds.Contains(c.Id))
                    .Select(_mapper.Map<Category>)
                    .ToList();
            }
        }
        
        return _mapper.Map<List<BlogPostDto>>(posts);
    }
}
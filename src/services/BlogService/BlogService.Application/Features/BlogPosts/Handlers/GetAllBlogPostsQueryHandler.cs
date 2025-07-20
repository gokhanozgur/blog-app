using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Features.BlogPosts.Queries;
using BlogService.Application.Interfaces.BlogPosts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.BlogPosts.Handlers;

public class GetAllBlogPostsQueryHandler : IRequestHandler<GetAllBlogPostsQuery, List<BlogPostDto>>
{
    private readonly IBlogPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;

    public GetAllBlogPostsQueryHandler(IBlogPostRepository repository, IMapper mapper,  ILogger<CreateBlogPostCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<BlogPostDto>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _repository.GetAllAsync();
        return _mapper.Map<List<BlogPostDto>>(posts);
    }
}
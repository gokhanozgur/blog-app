using AutoMapper;
using BlogService.Application.DTOs.Comments;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.Comments.Queries;
using BlogService.Application.Interfaces.Comments;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Comments.Handlers;

public class GetCommentBlogPostIdQueryHandler : IRequestHandler<GetCommentByBlogPostIdQuery, List<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCommentBlogPostIdQueryHandler> _logger;

    public GetCommentBlogPostIdQueryHandler(ICommentRepository commentRepository,  IMapper mapper, ILogger<GetCommentBlogPostIdQueryHandler> logger)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<CommentDto>> Handle(GetCommentByBlogPostIdQuery request, CancellationToken cancellationToken)
    {
        var comment =  await _commentRepository.GetByBlogPostIdAsync(request.BlogPostId);
        
        if (comment == null)
            throw new NotFoundException("Comment not found");
        
        _logger.LogInformation($"Blog post comments.");
        return _mapper.Map<List<CommentDto>>(comment);
    }
}
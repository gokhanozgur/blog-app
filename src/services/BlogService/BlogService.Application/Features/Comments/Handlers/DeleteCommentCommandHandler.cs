using AutoMapper;
using BlogService.Application.DTOs.Comments;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Application.Features.BlogPosts.Handlers;
using BlogService.Application.Features.Comments.Commands;
using BlogService.Application.Features.Comments.Queries;
using BlogService.Application.Interfaces.Comments;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Comments.Handlers;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCommentCommandHandler> _logger;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository,  IMapper mapper, ILogger<DeleteCommentCommandHandler> logger)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment =  await _commentRepository.GetByIdAsync(request.Id);
        
        if (comment == null)
            throw new NotFoundException("Comment not found");
        
        _logger.LogInformation($"Comment id {comment.Id} has been deleted.");
        return await _commentRepository.DeleteAsync(comment.Id);
    }
}
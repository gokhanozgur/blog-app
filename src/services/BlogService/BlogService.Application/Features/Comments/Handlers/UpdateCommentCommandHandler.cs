using AutoMapper;
using BlogService.Application.DTOs.Comments;
using BlogService.Application.Exceptions;
using BlogService.Application.Features.Comments.Commands;
using BlogService.Application.Interfaces.Comments;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Comments.Handlers;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCommentCommandHandler> _logger;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository,  IMapper mapper, ILogger<UpdateCommentCommandHandler> logger)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null)
            throw new NotFoundException("Comment not found");
        
        _mapper.Map(request.UpdateCommentDto, comment);
        comment.UpdatedAt = DateTime.UtcNow;
        
        await _commentRepository.UpdateAsync(comment);
        _logger.LogInformation($"{comment.Id} has been updated.");
        
        return _mapper.Map<CommentDto>(comment);
    }
}
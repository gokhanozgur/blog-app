using AutoMapper;
using BlogService.Application.DTOs.Comments;
using BlogService.Application.Features.Comments.Commands;
using BlogService.Application.Interfaces.Comments;
using BlogService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogService.Application.Features.Comments.Handlers;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommentCommandHandler> _logger;

    public CreateCommentCommandHandler(ICommentRepository commentRepository,  IMapper mapper, ILogger<CreateCommentCommandHandler> logger)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(request.CreateCommentDto);

        await _commentRepository.CreateAsync(comment);
        _logger.LogInformation("Comment created.");
        
        return _mapper.Map<CommentDto>(comment);
    }
}
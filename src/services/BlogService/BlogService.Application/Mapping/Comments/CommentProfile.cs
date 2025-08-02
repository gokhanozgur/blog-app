using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.DTOs.Comments;
using BlogService.Domain.Entities;

namespace BlogService.Application.Mapping.Comments;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();
    }
}
using AutoMapper;
using BlogService.Application.DTOs.BlogPosts;
using BlogService.Application.Features.BlogPosts.Commands;
using BlogService.Domain.Entities;

namespace BlogService.Application.Mapping.BlogPosts;

public class BlogPostProfile : Profile
{
    public BlogPostProfile()
    {
        CreateMap<BlogPost, BlogPostDto>().ReverseMap();
        CreateMap<CreateBlogPostDto, BlogPost>();
        CreateMap<UpdateBlogPostDto, BlogPost>();
    }
}
using AutoMapper;
using UserService.Application.DTOs;
using UserService.Application.DTOs.Users;
using UserService.Domain.Entities.Users;

namespace UserService.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
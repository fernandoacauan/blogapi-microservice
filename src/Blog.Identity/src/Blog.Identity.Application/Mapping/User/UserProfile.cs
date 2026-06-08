using AutoMapper;
using Blog.Identity.Application.Features.User.Commands.UpdateUser;
using Blog.Identity.Domain.Entities.User;

namespace Blog.Identity.Application.Mapping.User;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateUserCommand, UserEntity>().ForMember(dest => dest.HashedPassword, opt => opt.Ignore()).ReverseMap();
    }
}

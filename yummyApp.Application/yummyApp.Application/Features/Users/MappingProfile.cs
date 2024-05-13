using AutoMapper;
using yummyApp.Application.Dtos.Users;
using yummyApp.Application.Features.Users.Commands.Create;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<AppUser,CreateUserCommandRequest>().ReverseMap();
            CreateMap<AppUser, UserCreateDto>().ReverseMap();
            CreateMap<AppUser,UserUpdateDto>().ReverseMap();
            CreateMap<AppUser, UserReadDto>().ReverseMap();
        }
    }
}

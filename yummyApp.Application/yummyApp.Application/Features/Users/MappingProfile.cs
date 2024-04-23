using AutoMapper;
using yummyApp.Application.Features.Users.Commands.Create;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Users
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<User,CreateUserCommandRequest>().ReverseMap();
        }
    }
}

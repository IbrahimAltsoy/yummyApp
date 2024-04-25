using AutoMapper;
using yummyApp.Application.Features.Businesses.Queries.GetAll;
using yummyApp.Application.Features.FriendShips.Commands.Create;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.FriendShips
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        { 
            CreateMap<Friendship, CreateFrindShipCommandRequest>().ReverseMap();
            CreateMap<Friendship, GetAllBusinessQueryRequest>().ReverseMap();
        }
    }
}

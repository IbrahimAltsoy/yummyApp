using AutoMapper;
using yummyApp.Application.Features.Likes.Commands.Create;
using yummyApp.Application.Features.Likes.Commands.Delete;
using yummyApp.Application.Features.Likes.Queries.GetAll;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Likes
{
    internal class MappingProfile:Profile
    {
        public MappingProfile()
        { 
            CreateMap<Like, CreateLikecommandRequest>().ReverseMap();
            CreateMap<Like, GetAllLikeQueryRequest>().ReverseMap();
            CreateMap<Like, GetAllLikeQueryResponse>().ReverseMap();
            CreateMap<Like, DeleteLikeCommandRequest>().ReverseMap();
            
        }
    }

}

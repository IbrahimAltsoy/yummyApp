using AutoMapper;
using yummyApp.Application.Features.Posts.Commands.Create;
using yummyApp.Application.Features.Posts.Commands.Delete;
using yummyApp.Application.Features.Posts.Commands.Update;
using yummyApp.Application.Features.Posts.Queries.GetAll;
using yummyApp.Application.Features.Posts.Queries.GetById;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Posts
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        { 
            CreateMap<Post, CreatePostCommandRequest>().ReverseMap();
            CreateMap<Post, UpdatePostCommandRequest>().ReverseMap();
            CreateMap<Post, DeletePostCommandRequest>().ReverseMap();

            CreateMap<Post, GetAllPostQueryResponse>().ReverseMap();
            CreateMap<Post, GetByIdPostQueryResponse>().ReverseMap();
        }
    }
}

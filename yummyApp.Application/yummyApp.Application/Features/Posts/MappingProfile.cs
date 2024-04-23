using AutoMapper;
using yummyApp.Application.Features.Posts.Commands;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Posts
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        { 
            CreateMap<Post, CreatePostCommandRequest>().ReverseMap();
        }
    }
}

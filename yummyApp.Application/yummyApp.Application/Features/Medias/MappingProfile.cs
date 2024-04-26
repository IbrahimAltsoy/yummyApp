using AutoMapper;
using yummyApp.Application.Features.Medias.Commands.Create;
using yummyApp.Application.Features.Medias.Commands.Delete;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Medias
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        { 
            CreateMap<Media, CreateMediaCommandRequest>().ReverseMap();
            //CreateMap<Media, DeleteMediaCommandRequest>().ReverseMap();
        }
    }
}

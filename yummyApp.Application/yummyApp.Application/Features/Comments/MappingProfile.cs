using AutoMapper;
using yummyApp.Application.Features.Comments.Commands.Create;
using yummyApp.Application.Features.Comments.Commands.Delete;
using yummyApp.Application.Features.Comments.Commands.Update;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CreateCommentCommandRequest>().ReverseMap();
            CreateMap<Comment, UpdateCommentCommandRequest>().ReverseMap();
            CreateMap<Comment, DeleteCommentCommandRequest>().ReverseMap();
        }
    }
}

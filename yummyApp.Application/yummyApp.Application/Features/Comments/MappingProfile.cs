using AutoMapper;
using yummyApp.Application.Features.Comments.Commands.Create;
using yummyApp.Application.Features.Comments.Commands.Delete;
using yummyApp.Application.Features.Comments.Commands.Update;
using yummyApp.Application.Features.Comments.Queries.GetAll;
using yummyApp.Application.Features.Comments.Queries.GetById;
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
            //CreateMap<Comment, GetAllCommentCommandResponse>().ReverseMap();
            CreateMap<Comment, GetAllCommentQueryResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.UserSurname, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.PostContent, opt => opt.MapFrom(src => src.Post.Content));
            CreateMap<Comment, GetByIdCommentQueryResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.UserSurname, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.PostContent, opt => opt.MapFrom(src => src.Post.Content));
        }
    }
}

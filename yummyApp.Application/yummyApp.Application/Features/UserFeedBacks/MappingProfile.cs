using AutoMapper;
using yummyApp.Application.Features.UserFeedBacks.Commands.Create;
using yummyApp.Application.Features.UserFeedBacks.Commands.Delete;
using yummyApp.Application.Features.UserFeedBacks.Commands.Update;
using yummyApp.Application.Features.UserFeedBacks.Queries.Get;
using yummyApp.Application.Features.UserFeedBacks.Queries.GetById;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.UserFeedBacks
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserFeedback, CreateUserFeedBackCommandRequest>().ReverseMap();
            CreateMap<UserFeedback, UpdateUserFeedBackCommandRequest>().ReverseMap();
            CreateMap<UserFeedback, DeleteUserFeedBackCommandRequest>().ReverseMap();

            CreateMap<UserFeedback, GetAllUserFeedBackQueryResponse>()
                .ForMember(x => x.UserName, x => x.MapFrom(x => x.User.Name+" " +x.User.Surname)).ReverseMap();
                
            CreateMap<UserFeedback, GetByIdUserFeedBackQueryResponse>().ReverseMap();

                
        }
    }
}

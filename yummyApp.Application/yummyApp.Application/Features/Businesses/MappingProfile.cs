using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using yummyApp.Application.Features.Businesses.Commands.CreateBusiness;
using yummyApp.Application.Features.Businesses.Commands.DeleteBusiness;
using yummyApp.Application.Features.Businesses.Commands.UpdateBusiness;
using yummyApp.Application.Features.Businesses.Queries.GetAll;
using yummyApp.Application.Features.Businesses.Queries.GetById;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Businesses
{
    public class MappingProfile:Profile
    {
      public MappingProfile()
        {
            CreateMap<Business, CreateBusinessCommand>().ReverseMap();
            CreateMap<Business, UpdateBusinessCommand>().ReverseMap();

           // CreateMap<Business, GetAllBusinessQueryRequest>().ReverseMap();
            CreateMap<Business, GetAllBusinessQueryResponse>().ReverseMap();

            CreateMap<Business, GetByIdQueryRequest>().ReverseMap();
            CreateMap<Business, GetByIdQueryResponse>().ReverseMap();
            CreateMap<Business, DeleteBusinessRequest>().ReverseMap();
            CreateMap<Business, DeleteBusinessResponse>().ReverseMap();
         
        }
    }
}

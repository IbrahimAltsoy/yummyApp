using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using yummyApp.Application.Features.Businesses.Commands.CreateBusiness;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Businesses
{
    public class MappingProfile:Profile
    {
      public MappingProfile()
        {
            CreateMap<Business, CreateBusinessCommand>().ReverseMap();
         
        }
    }
}

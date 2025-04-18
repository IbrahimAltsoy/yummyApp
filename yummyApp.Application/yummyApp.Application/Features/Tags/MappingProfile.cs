﻿using AutoMapper;
using yummyApp.Application.Features.Tags.Commands.Create;
using yummyApp.Application.Features.Tags.Commands.Update;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Tags
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Tag, CreateTagCommandRequest>().ReverseMap();
            CreateMap<Tag, UpdateTagCommandRequest>().ReverseMap();
        }
    }
}

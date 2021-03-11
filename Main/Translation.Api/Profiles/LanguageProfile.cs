// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Profiles
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Language, LanguageDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<LanguageForCreationDto, Language>()
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<LanguageForUpdatingDto, Language>()
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

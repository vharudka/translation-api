// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using System;

namespace Harudka.Translation.Api.Profiles
{
    public class ApplicationLanguageProfile : Profile
    {
        public ApplicationLanguageProfile()
        {
            CreateMap<ApplicationLanguage, ApplicationLanguageDto>()
                .ForMember(d => d.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(d => d.ApplicationName, opt => opt.MapFrom(src => src.Application.Name))
                .ForMember(d => d.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                .ForMember(d => d.LanguageCode, opt => opt.MapFrom(src => src.Language.Code))
                .ForMember(d => d.LanguageName, opt => opt.MapFrom(src => src.Language.Name));

            CreateMap<ApplicationLanguageForCreationDto, ApplicationLanguage>()
                .ForMember(d => d.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<string, ApplicationLanguage>()
                .ForMember(d => d.ApplicationId, opt => opt.MapFrom(src => new Guid(src)))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

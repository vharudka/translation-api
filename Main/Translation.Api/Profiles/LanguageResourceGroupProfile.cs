// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using System;

namespace Harudka.Translation.Api.Profiles
{
    public class LanguageResourceGroupProfile : Profile
    {
        public LanguageResourceGroupProfile()
        {
            CreateMap<LanguageResourceGroup, LanguageResourceGroupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<LanguageResourceGroupForCreationDto, LanguageResourceGroup>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<string, LanguageResourceGroup>()
                .ForMember(d => d.ApplicationId, opt => opt.MapFrom(src => new Guid(src)))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

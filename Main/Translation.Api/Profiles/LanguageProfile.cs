﻿// Copyright 2020, Vladislav Harudka. All rights reserved.
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
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageForCreationDto, Language>();
            CreateMap<LanguageForUpdatingDto, Language>();
        }
    }
}

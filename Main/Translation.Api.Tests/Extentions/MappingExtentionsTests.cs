// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Extentions;
using Harudka.Translation.Api.Tests.Data;
using System;
using Xunit;

namespace Harudka.Translation.Api.Tests.Extentions
{
    public class MappingExtentionsTests
    {
        private readonly ApplicationLanguageForCreationDtoBuilder _applicationLanguageForCreationDtoBuilder;
        private readonly IMapper _mapper;

        public MappingExtentionsTests()
        {
            _applicationLanguageForCreationDtoBuilder = new ApplicationLanguageForCreationDtoBuilder();

            var config = new MapperConfiguration(options =>
            {
                options.CreateMap<ApplicationLanguageForCreationDto, ApplicationLanguage>()
                       .ForMember(d => d.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                       .ForAllOtherMembers(opt => opt.Ignore());

                options.CreateMap<string, ApplicationLanguage>()
                       .ForMember(d => d.ApplicationId, opt => opt.MapFrom(src => new Guid(src)))
                       .ForAllOtherMembers(opt => opt.Ignore());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguageForCreationDto = _applicationLanguageForCreationDtoBuilder.WithLanguageId(1)
                                                                                             .Build();

            var applicationLanguage = _mapper.Map<ApplicationLanguage>(applicationId.ToString(), applicationLanguageForCreationDto);

            Assert.Equal(applicationId, applicationLanguage.ApplicationId);
            Assert.Equal(applicationLanguageForCreationDto.LanguageId, applicationLanguage.LanguageId);
        }
    }
}

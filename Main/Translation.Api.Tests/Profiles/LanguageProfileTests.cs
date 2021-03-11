// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Profiles;
using Harudka.Translation.Api.Tests.Data;
using Xunit;

namespace Harudka.Translation.Api.Tests.Profiles
{
    public class LanguageProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        private readonly LanguageBuilder _languageBuilder;
        private readonly LanguageForCreationDtoBuilder _languageForCreationDtoBuilder;
        private readonly LanguageForUpdatingDtoBuilder _languageForUpdatingDtoBuilder;

        public LanguageProfileTests()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<LanguageProfile>());
            _mapper = _config.CreateMapper();

            _languageBuilder = new LanguageBuilder();
            _languageForCreationDtoBuilder = new LanguageForCreationDtoBuilder();
            _languageForUpdatingDtoBuilder = new LanguageForUpdatingDtoBuilder();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void ConvertFromLanguageToLanguageDto_IsValid()
        {
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            var result = _mapper.Map<Language, LanguageDto>(language);

            Assert.Equal(language.Id, result.Id);
            Assert.Equal(language.Code, result.Code);
            Assert.Equal(language.Name, result.Name);
        }

        [Fact]
        public void ConvertFromLanguageForCreationDtoToLanguage_IsValid()
        {
            var languageForCreationDto = _languageForCreationDtoBuilder.WithCode("en")
                                                                       .WithName("English")
                                                                       .Build();

            var result = _mapper.Map<LanguageForCreationDto, Language>(languageForCreationDto);

            Assert.Equal(languageForCreationDto.Code, result.Code);
            Assert.Equal(languageForCreationDto.Name, result.Name);
        }

        [Fact]
        public void ConvertFromLanguageForUpdatingDtoToLanguage_IsValid()
        {
            var languageForUpdatingDto = _languageForUpdatingDtoBuilder.WithCode("en")
                                                                       .WithName("English")
                                                                       .Build();

            var result = _mapper.Map<LanguageForUpdatingDto, Language>(languageForUpdatingDto);

            Assert.Equal(languageForUpdatingDto.Code, result.Code);
            Assert.Equal(languageForUpdatingDto.Name, result.Name);
        }
    }
}

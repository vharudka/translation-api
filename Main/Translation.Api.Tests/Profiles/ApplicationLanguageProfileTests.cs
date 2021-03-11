// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Profiles;
using Harudka.Translation.Api.Tests.Data;
using System;
using Xunit;

namespace Harudka.Translation.Api.Tests.Profiles
{
    public class ApplicationLanguageProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        private readonly ApplicationBuilder _applicationBuilder;
        private readonly LanguageBuilder _languageBuilder;
        private readonly ApplicationLanguageBuilder _applicationLanguageBuilder;
        private readonly ApplicationLanguageForCreationDtoBuilder _applicationLanguageForCreationDtoBuilder;

        public ApplicationLanguageProfileTests()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationLanguageProfile>());
            _mapper = _config.CreateMapper();

            _applicationBuilder = new ApplicationBuilder();
            _languageBuilder = new LanguageBuilder();
            _applicationLanguageBuilder = new ApplicationLanguageBuilder();
            _applicationLanguageForCreationDtoBuilder = new ApplicationLanguageForCreationDtoBuilder();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void ConvertFromApplicationLanguageToApplicationLanguageDto_IsValid()
        {
            var application = _applicationBuilder.WithId(Guid.NewGuid())
                                                 .WithName("TestApplication")
                                                 .Build();

            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(Guid.NewGuid())
                                                                 .WithApplication(application)
                                                                 .WithLanguage(language)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            var result = _mapper.Map<ApplicationLanguage, ApplicationLanguageDto>(applicationLanguage);

            Assert.Equal(applicationLanguage.ApplicationId, result.ApplicationId);
            Assert.Equal(applicationLanguage.Application.Name, result.ApplicationName);
            Assert.Equal(applicationLanguage.LanguageId, result.LanguageId);
            Assert.Equal(applicationLanguage.Language.Code, result.LanguageCode);
            Assert.Equal(applicationLanguage.Language.Name, result.LanguageName);
        }

        [Fact]
        public void ConvertFromApplicationLanguageForCreationDtoToApplicationLanguage_IsValid()
        {
            var applicationLanguageForCreationDto = _applicationLanguageForCreationDtoBuilder.WithLanguageId(1)
                                                                                             .Build();

            var result = _mapper.Map<ApplicationLanguageForCreationDto, ApplicationLanguage>(applicationLanguageForCreationDto);

            Assert.Equal(applicationLanguageForCreationDto.LanguageId, result.LanguageId);
        }

        [Fact]
        public void ConvertFromStringToApplicationLanguage_IsValid()
        {
            var applicationId = Guid.NewGuid();

            var result = _mapper.Map<string, ApplicationLanguage>(applicationId.ToString());

            Assert.Equal(applicationId, result.ApplicationId);
        }
    }
}

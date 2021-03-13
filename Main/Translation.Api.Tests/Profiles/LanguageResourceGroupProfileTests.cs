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
    public class LanguageResourceGroupProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        private readonly ApplicationBuilder _applicationBuilder;
        private readonly LanguageResourceGroupBuilder _languageResourceGroupBuilder;
        private readonly LanguageResourceGroupForCreationDtoBuilder _languageResourceGroupForCreationDtoBuilder;

        public LanguageResourceGroupProfileTests()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<LanguageResourceGroupProfile>());
            _mapper = _config.CreateMapper();

            _applicationBuilder = new ApplicationBuilder();
            _languageResourceGroupBuilder = new LanguageResourceGroupBuilder();
            _languageResourceGroupForCreationDtoBuilder = new LanguageResourceGroupForCreationDtoBuilder();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void ConvertFromLanguageResourceGroupToLanguageResourceGroupDto_IsValid()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("TestApplication")
                                                 .Build();

            var languageResourceGroup = _languageResourceGroupBuilder.WithId(1)
                                                                     .WithName("TestLanguageResourceGroup")
                                                                     .WithApplicationId(applicationId)
                                                                     .WithApplication(application)
                                                                     .Build();

            var result = _mapper.Map<LanguageResourceGroup, LanguageResourceGroupDto>(languageResourceGroup);

            Assert.Equal(languageResourceGroup.Id, result.Id);
            Assert.Equal(languageResourceGroup.Name, result.Name);
        }

        [Fact]
        public void ConvertFromApplicationLanguageForCreationDtoToApplicationLanguage_IsValid()
        {
            var languageResourceGroupForCreationDto = _languageResourceGroupForCreationDtoBuilder.WithName("TestLanguageResourceGroup")
                                                                                                 .Build();

            var result = _mapper.Map<LanguageResourceGroupForCreationDto, LanguageResourceGroup>(languageResourceGroupForCreationDto);

            Assert.Equal(languageResourceGroupForCreationDto.Name, result.Name);
        }

        [Fact]
        public void ConvertFromStringToApplicationLanguage_IsValid()
        {
            var applicationId = Guid.NewGuid();

            var result = _mapper.Map<string, LanguageResourceGroup>(applicationId.ToString());

            Assert.Equal(applicationId, result.ApplicationId);
        }
    }
}

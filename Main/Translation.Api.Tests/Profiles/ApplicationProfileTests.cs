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
    public class ApplicationProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        private readonly ApplicationBuilder _applicationBuilder;
        private readonly ApplicationForCreationDtoBuilder _applicationForCreationDtoBuilder;
        private readonly ApplicationForUpdatingDtoBuilder _applicationForUpdatingDtoBuilder;

        public ApplicationProfileTests()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());
            _mapper = _config.CreateMapper();

            _applicationBuilder = new ApplicationBuilder();
            _applicationForCreationDtoBuilder = new ApplicationForCreationDtoBuilder();
            _applicationForUpdatingDtoBuilder = new ApplicationForUpdatingDtoBuilder();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void ConvertFromApplicationToApplicationDto_IsValid()
        {
            var application = _applicationBuilder.WithId(Guid.NewGuid())
                                                 .WithName("TestApplication")
                                                 .Build();

            var result = _mapper.Map<Application, ApplicationDto>(application);

            Assert.Equal(application.Id, result.Id);
            Assert.Equal(application.Name, result.Name);
        }

        [Fact]
        public void ConvertFromApplicationForCreationDtoToApplication_IsValid()
        {
            var applicationForCreationDto = _applicationForCreationDtoBuilder.WithName("TestApplication")
                                                                             .Build();

            var result = _mapper.Map<ApplicationForCreationDto, Application>(applicationForCreationDto);

            Assert.Equal(applicationForCreationDto.Name, result.Name);
        }

        [Fact]
        public void ConvertFromApplicationForUpdatingDtoToApplication_IsValid()
        {
            var applicationForUpdatingDto = _applicationForUpdatingDtoBuilder.WithName("TestApplication")
                                                                             .Build();

            var result = _mapper.Map<ApplicationForUpdatingDto, Application>(applicationForUpdatingDto);

            Assert.Equal(applicationForUpdatingDto.Name, result.Name);
        }
    }
}

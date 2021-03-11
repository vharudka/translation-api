// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Controllers;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Repository;
using Harudka.Translation.Api.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Harudka.Translation.Api.Tests.Controllers
{
    public class ApplicationsControllerTests
    {
        private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
        private readonly Mock<IApplicationLanguageRepository> _applicationLanguageRepositoryMock;

        private readonly ApplicationBuilder _applicationBuilder;
        private readonly ApplicationForCreationDtoBuilder _applicationForCreationDtoBuilder;
        private readonly ApplicationForUpdatingDtoBuilder _applicationForUpdatingDtoBuilder;

        private readonly ApplicationLanguageBuilder _applicationLanguageBuilder;
        private readonly ApplicationLanguageDtoBuilder _applicationLanguageDtoBuilder;
        private readonly ApplicationLanguageForCreationDtoBuilder _applicationLanguageForCreationDtoBuilder;
        
        private readonly ApplicationsController _controller;

        public ApplicationsControllerTests()
        {
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _applicationLanguageRepositoryMock = new Mock<IApplicationLanguageRepository>();

            _applicationBuilder = new ApplicationBuilder();
            _applicationForCreationDtoBuilder = new ApplicationForCreationDtoBuilder();
            _applicationForUpdatingDtoBuilder = new ApplicationForUpdatingDtoBuilder();

            _applicationLanguageBuilder = new ApplicationLanguageBuilder();
            _applicationLanguageDtoBuilder = new ApplicationLanguageDtoBuilder();
            _applicationLanguageForCreationDtoBuilder = new ApplicationLanguageForCreationDtoBuilder();

            var config = new MapperConfiguration(options =>
            {
                options.CreateMap<Application, ApplicationDto>();
                options.CreateMap<ApplicationForCreationDto, Application>();
                options.CreateMap<ApplicationForUpdatingDto, Application>();

                options.CreateMap<ApplicationLanguage, ApplicationLanguageDto>()
                       .ForMember(d => d.ApplicationId, opt => opt.MapFrom(src => src.ApplicationId))
                       .ForMember(d => d.ApplicationName, opt => opt.MapFrom(src => src.Application.Name))
                       .ForMember(d => d.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                       .ForMember(d => d.LanguageCode, opt => opt.MapFrom(src => src.Language.Code))
                       .ForMember(d => d.LanguageName, opt => opt.MapFrom(src => src.Language.Name));

                options.CreateMap<ApplicationLanguageForCreationDto, ApplicationLanguage>()
                       .ForMember(d => d.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                       .ForAllOtherMembers(opt => opt.Ignore());

                options.CreateMap<string, ApplicationLanguage>()
                       .ForMember(d => d.ApplicationId, opt => opt.MapFrom(src => new Guid(src)))
                       .ForAllOtherMembers(opt => opt.Ignore());

            });
            var mapper = config.CreateMapper();

            var httpContext = new DefaultHttpContext();
            _controller = new ApplicationsController(_applicationRepositoryMock.Object, _applicationLanguageRepositoryMock.Object, mapper)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public void GetOptions_ReturnsOkResult()
        {
            var languages = new List<Application>();

            var result = _controller.GetOptions();

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateApplicationAsync_ReturnsCreatedResponse()
        {
            var applicationForCreationDto = _applicationForCreationDtoBuilder.WithName("English")
                                                                             .Build();
            var application = _applicationBuilder.WithId(Guid.NewGuid())
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Application>()))
                                      .ReturnsAsync(application);

            var result = await _controller.CreateApplicationAsync(applicationForCreationDto);

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async Task CreateApplicationAsync_ReturnsCreatedItem()
        {
            var applicationId = Guid.NewGuid();
            var applicationForCreationDto = _applicationForCreationDtoBuilder.WithName("English")
                                                                             .Build();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Application>()))
                                      .ReturnsAsync(application);

            var result = await _controller.CreateApplicationAsync(applicationForCreationDto);

            var okObjectResult = result.Result as CreatedAtRouteResult;

            var item = Assert.IsType<ApplicationDto>(okObjectResult.Value);
            Assert.Equal(application.Id, item.Id);
            Assert.Equal(application.Name, item.Name);
        }

        [Fact]
        public async Task UpdateApplicationAsync_ReturnsNotFoundResult()
        {
            Application application = null;
            var applicationForUpdating = _applicationForUpdatingDtoBuilder.WithName("English")
                                                                          .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.UpdateApplicationAsync(Guid.NewGuid(), applicationForUpdating);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateApplicationAsync_ReturnsNoContentResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationForUpdating = _applicationForUpdatingDtoBuilder.WithName("English")
                                                                          .Build();
            var language = _applicationBuilder.WithId(applicationId)
                                              .WithName("English")
                                              .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(language);
            _applicationRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Application>()))
                                      .Verifiable();

            var result = await _controller.UpdateApplicationAsync(applicationId, applicationForUpdating);

            _applicationRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetApplicationAsync_ReturnsNotFoundResult()
        {
            Application application = null;

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.GetApplicationAsync(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetApplicationAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.GetApplicationAsync(applicationId);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetApplicationAsync_ReturnsRightItem()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.GetApplicationAsync(applicationId);

            var okObjectResult = result.Result as OkObjectResult;

            var item = Assert.IsType<ApplicationDto>(okObjectResult.Value);
            Assert.Equal(application.Id, item.Id);
            Assert.Equal(application.Name, item.Name);
        }

        [Fact]
        public async Task GetAllApplicationsAsync_ReturnsOkResult()
        {
            var applications = new List<Application>();

            _applicationRepositoryMock.Setup(x => x.GetAllAsync())
                                      .ReturnsAsync(applications);

            var result = await _controller.GetAllApplicationsAsync();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllApplicationsAsync_ReturnsAllItems()
        {
            var applications = new List<Application>
            {
                _applicationBuilder.WithId(Guid.NewGuid())
                                   .WithName("English")
                                   .Build(),
                _applicationBuilder.WithId(Guid.NewGuid())
                                   .WithName("Polish")
                                   .Build(),
            };

            _applicationRepositoryMock.Setup(x => x.GetAllAsync())
                                      .ReturnsAsync(applications);

            var result = await _controller.GetAllApplicationsAsync();

            var okObjectResult = result.Result as OkObjectResult;

            var items = Assert.IsAssignableFrom<IReadOnlyList<ApplicationDto>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task DeleteApplicationAsync_ReturnsNotFoundResult()
        {
            Application application = null;

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.DeleteApplicationAsync(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteApplicationAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);
            _applicationRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Application>()))
                                      .Verifiable();

            var result = await _controller.DeleteApplicationAsync(applicationId);

            _applicationRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CreateApplicationLanguageAsync_ReturnsCreatedResponse()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguageForCreationDto = _applicationLanguageForCreationDtoBuilder.WithLanguageId(1)
                                                                                             .Build();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationLanguage>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.CreateApplicationLanguageAsync(applicationId.ToString(), applicationLanguageForCreationDto);

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async Task CreateApplicationLanguageAsync_ReturnsCreatedItem()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguageForCreationDto = _applicationLanguageForCreationDtoBuilder.WithLanguageId(1)
                                                                                             .Build();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationLanguage>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.CreateApplicationLanguageAsync(applicationId.ToString(), applicationLanguageForCreationDto);

            var okObjectResult = result.Result as CreatedAtRouteResult;

            var item = Assert.IsType<ApplicationLanguageDto>(okObjectResult.Value);
            Assert.Equal(applicationLanguage.ApplicationId, item.ApplicationId);
            Assert.Equal(applicationLanguage.LanguageId, item.LanguageId);
        }

        [Fact]
        public async Task GetApplicationLanguageAsync_ReturnsNotFoundResult()
        {
            ApplicationLanguage application = null;

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(application);

            var result = await _controller.GetApplicationLanguageAsync(Guid.NewGuid(), 1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetApplicationLanguageAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.GetApplicationLanguageAsync(applicationId, 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetApplicationLanguageAsync_ReturnsRightItem()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.GetApplicationLanguageAsync(applicationId, 1);

            var okObjectResult = result.Result as OkObjectResult;

            var item = Assert.IsType<ApplicationLanguageDto>(okObjectResult.Value);
            Assert.Equal(applicationLanguage.ApplicationId, item.ApplicationId);
            Assert.Equal(applicationLanguage.LanguageId, item.LanguageId);
        }

        [Fact]
        public async Task GetAllApplicationLanguagesAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguages = new List<ApplicationLanguage>();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAllAsync(applicationId))
                                              .ReturnsAsync(applicationLanguages);

            var result = await _controller.GetAllApplicationsAsync();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllApplicationLanguagesAsync_ReturnsAllItems()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguages = new List<ApplicationLanguage>
            {
                _applicationLanguageBuilder.WithApplicationId(applicationId)
                                           .WithLanguageId(1)
                                           .Build(),
                _applicationLanguageBuilder.WithApplicationId(applicationId)
                                           .WithLanguageId(2)
                                           .Build(),
            };

            _applicationLanguageRepositoryMock.Setup(x => x.GetAllAsync(applicationId))
                                              .ReturnsAsync(applicationLanguages);

            var result = await _controller.GetAllApplicationLanguagesAsync(applicationId);

            var okObjectResult = result.Result as OkObjectResult;

            var items = Assert.IsAssignableFrom<IReadOnlyList<ApplicationLanguageDto>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task DeleteApplicationLanguageAsync_ReturnsNotFoundResult()
        {
            ApplicationLanguage applicationLanguage = null;

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.DeleteApplicationAsync(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteApplicationLanguageAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);
            _applicationLanguageRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ApplicationLanguage>()))
                                              .Verifiable();

            var result = await _controller.DeleteApplicationAsync(applicationId);

            _applicationRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Controllers;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Profiles;
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
    public class ApplicationLanguageControllerTests
    {
        private readonly Mock<IApplicationLanguageRepository> _applicationLanguageRepositoryMock;

        private readonly ApplicationLanguageBuilder _applicationLanguageBuilder;
        private readonly ApplicationLanguageForCreationDtoBuilder _applicationLanguageForCreationDtoBuilder;

        private readonly ApplicationLanguageController _controller;

        public ApplicationLanguageControllerTests()
        {
            _applicationLanguageRepositoryMock = new Mock<IApplicationLanguageRepository>();

            _applicationLanguageBuilder = new ApplicationLanguageBuilder();
            _applicationLanguageForCreationDtoBuilder = new ApplicationLanguageForCreationDtoBuilder();

            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<ApplicationLanguageProfile>();

            });
            var mapper = config.CreateMapper();

            var httpContext = new DefaultHttpContext();
            _controller = new ApplicationLanguageController(_applicationLanguageRepositoryMock.Object, mapper)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedResponse()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguageForCreationDto = _applicationLanguageForCreationDtoBuilder.WithLanguageId(1)
                                                                                             .Build();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationLanguage>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.CreateAsync(applicationId.ToString(), applicationLanguageForCreationDto);

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedItem()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguageForCreationDto = _applicationLanguageForCreationDtoBuilder.WithLanguageId(1)
                                                                                             .Build();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationLanguage>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.CreateAsync(applicationId.ToString(), applicationLanguageForCreationDto);

            var okObjectResult = result.Result as CreatedAtRouteResult;

            var item = Assert.IsType<ApplicationLanguageDto>(okObjectResult.Value);
            Assert.Equal(applicationLanguage.ApplicationId, item.ApplicationId);
            Assert.Equal(applicationLanguage.LanguageId, item.LanguageId);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFoundResult()
        {
            ApplicationLanguage application = null;

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(application);

            var result = await _controller.GetAsync(Guid.NewGuid(), 1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.GetAsync(applicationId, 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsRightItem()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.GetAsync(applicationId, 1);

            var okObjectResult = result.Result as OkObjectResult;

            var item = Assert.IsType<ApplicationLanguageDto>(okObjectResult.Value);
            Assert.Equal(applicationLanguage.ApplicationId, item.ApplicationId);
            Assert.Equal(applicationLanguage.LanguageId, item.LanguageId);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguages = new List<ApplicationLanguage>();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAllAsync(applicationId))
                                              .ReturnsAsync(applicationLanguages);

            var result = await _controller.GetAllAsync(applicationId);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllItems()
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

            var result = await _controller.GetAllAsync(applicationId);

            var okObjectResult = result.Result as OkObjectResult;

            var items = Assert.IsAssignableFrom<IReadOnlyList<ApplicationLanguageDto>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNotFoundResult()
        {
            ApplicationLanguage applicationLanguage = null;

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);

            var result = await _controller.DeleteAsync(Guid.NewGuid(), 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var applicationLanguage = _applicationLanguageBuilder.WithApplicationId(applicationId)
                                                                 .WithLanguageId(1)
                                                                 .Build();

            _applicationLanguageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<short>()))
                                              .ReturnsAsync(applicationLanguage);
            _applicationLanguageRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ApplicationLanguage>()))
                                              .Verifiable();

            var result = await _controller.DeleteAsync(applicationId, 1);

            _applicationLanguageRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }
    }
}

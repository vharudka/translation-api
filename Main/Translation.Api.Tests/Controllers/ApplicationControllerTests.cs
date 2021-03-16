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
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Harudka.Translation.Api.Tests.Controllers
{
    public class ApplicationControllerTests
    {
        private readonly Mock<IApplicationRepository> _applicationRepositoryMock;

        private readonly ApplicationBuilder _applicationBuilder;
        private readonly ApplicationForCreationDtoBuilder _applicationForCreationDtoBuilder;
        private readonly ApplicationForUpdatingDtoBuilder _applicationForUpdatingDtoBuilder;

        private readonly ApplicationController _controller;

        public ApplicationControllerTests()
        {
            _applicationRepositoryMock = new Mock<IApplicationRepository>();

            _applicationBuilder = new ApplicationBuilder();
            _applicationForCreationDtoBuilder = new ApplicationForCreationDtoBuilder();
            _applicationForUpdatingDtoBuilder = new ApplicationForUpdatingDtoBuilder();

            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<ApplicationProfile>();

            });
            var mapper = config.CreateMapper();

            var httpContext = new DefaultHttpContext();
            _controller = new ApplicationController(_applicationRepositoryMock.Object, mapper)
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
        public async Task CreateAsync_ReturnsCreatedResponse()
        {
            var applicationForCreationDto = _applicationForCreationDtoBuilder.WithName("English")
                                                                             .Build();
            var application = _applicationBuilder.WithId(Guid.NewGuid())
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Application>()))
                                      .ReturnsAsync(application);

            var result = await _controller.CreateAsync(applicationForCreationDto);

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedItem()
        {
            var applicationId = Guid.NewGuid();
            var applicationForCreationDto = _applicationForCreationDtoBuilder.WithName("English")
                                                                             .Build();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Application>()))
                                      .ReturnsAsync(application);

            var result = await _controller.CreateAsync(applicationForCreationDto);

            var okObjectResult = result.Result as CreatedAtRouteResult;

            var item = Assert.IsType<ApplicationDto>(okObjectResult.Value);
            Assert.Equal(application.Id, item.Id);
            Assert.Equal(application.Name, item.Name);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNotFoundResult()
        {
            Application application = null;
            var applicationForUpdating = _applicationForUpdatingDtoBuilder.WithName("English")
                                                                          .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.UpdateAsync(Guid.NewGuid(), applicationForUpdating);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNoContentResult()
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

            var result = await _controller.UpdateAsync(applicationId, applicationForUpdating);

            _applicationRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFoundResult()
        {
            Application application = null;

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.GetAsync(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.GetAsync(applicationId);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsRightItem()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.GetAsync(applicationId);

            var okObjectResult = result.Result as OkObjectResult;

            var item = Assert.IsType<ApplicationDto>(okObjectResult.Value);
            Assert.Equal(application.Id, item.Id);
            Assert.Equal(application.Name, item.Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult()
        {
            var applications = new List<Application>();

            _applicationRepositoryMock.Setup(x => x.GetAllAsync())
                                      .ReturnsAsync(applications);

            var result = await _controller.GetAllAsync();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllItems()
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

            var result = await _controller.GetAllAsync();

            var okObjectResult = result.Result as OkObjectResult;

            var items = Assert.IsAssignableFrom<IReadOnlyList<ApplicationDto>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNotFoundResult()
        {
            Application application = null;

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);

            var result = await _controller.DeleteAsync(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("English")
                                                 .Build();

            _applicationRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(application);
            _applicationRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Application>()))
                                      .Verifiable();

            var result = await _controller.DeleteAsync(applicationId);

            _applicationRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }
    }
}

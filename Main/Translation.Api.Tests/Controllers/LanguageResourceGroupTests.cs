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
    public class LanguageResourceGroupTests
    {
        private readonly Mock<ILanguageResourceGroupRepository> _languageResourceGroupRepositoryMock;

        private readonly ApplicationBuilder _applicationBuilder;

        private readonly LanguageResourceGroupBuilder _languageResourceGroupBuilder;
        private readonly LanguageResourceGroupForCreationDtoBuilder _languageResourceGroupForCreationDtoBuilder;

        private readonly LanguageResourceGroupController _controller;

        public LanguageResourceGroupTests()
        {
            _languageResourceGroupRepositoryMock = new Mock<ILanguageResourceGroupRepository>();

            _applicationBuilder = new ApplicationBuilder();

            _languageResourceGroupBuilder = new LanguageResourceGroupBuilder();
            _languageResourceGroupForCreationDtoBuilder = new LanguageResourceGroupForCreationDtoBuilder();

            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<LanguageResourceGroupProfile>();

            });
            var mapper = config.CreateMapper();

            var httpContext = new DefaultHttpContext();
            _controller = new LanguageResourceGroupController(_languageResourceGroupRepositoryMock.Object, mapper)
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
            var languageResourceGroupName = "TestLanguageResourceGroup";
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("TestApplication")
                                                 .Build();
            var languageResourceGroup = _languageResourceGroupBuilder.WithId(1)
                                                                     .WithName(languageResourceGroupName)
                                                                     .WithApplicationId(applicationId)
                                                                     .WithApplication(application)
                                                                     .Build();
            var languageResourceGroupForCreationDto = _languageResourceGroupForCreationDtoBuilder.WithName(languageResourceGroupName)
                                                                                                 .Build();

            _languageResourceGroupRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<LanguageResourceGroup>()))
                                                .ReturnsAsync(languageResourceGroup);

            var result = await _controller.CreateAsync(applicationId.ToString(), languageResourceGroupForCreationDto);

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedItem()
        {
            var applicationId = Guid.NewGuid();
            var languageResourceGroupName = "TestLanguageResourceGroup";
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("TestApplication")
                                                 .Build();
            var languageResourceGroup = _languageResourceGroupBuilder.WithId(1)
                                                                     .WithName(languageResourceGroupName)
                                                                     .WithApplicationId(applicationId)
                                                                     .WithApplication(application)
                                                                     .Build();
            var languageResourceGroupForCreationDto = _languageResourceGroupForCreationDtoBuilder.WithName(languageResourceGroupName)
                                                                                                 .Build();

            _languageResourceGroupRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<LanguageResourceGroup>()))
                                                .ReturnsAsync(languageResourceGroup);

            var result = await _controller.CreateAsync(applicationId.ToString(), languageResourceGroupForCreationDto);

            var okObjectResult = result.Result as CreatedAtRouteResult;

            var item = Assert.IsType<LanguageResourceGroupDto>(okObjectResult.Value);
            Assert.Equal(languageResourceGroup.Id, item.Id);
            Assert.Equal(languageResourceGroup.Name, item.Name);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFoundResult()
        {
            LanguageResourceGroup languageResourceGroup = null;

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                                                .ReturnsAsync(languageResourceGroup);

            var result = await _controller.GetAsync(Guid.NewGuid(), 1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var languageResourceGroupName = "TestLanguageResourceGroup";
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("TestApplication")
                                                 .Build();
            var languageResourceGroup = _languageResourceGroupBuilder.WithId(1)
                                                                     .WithName(languageResourceGroupName)
                                                                     .WithApplicationId(applicationId)
                                                                     .WithApplication(application)
                                                                     .Build();

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                                                .ReturnsAsync(languageResourceGroup);

            var result = await _controller.GetAsync(applicationId, 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsRightItem()
        {
            var applicationId = Guid.NewGuid();
            var languageResourceGroupName = "TestLanguageResourceGroup";
            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("TestApplication")
                                                 .Build();
            var languageResourceGroup = _languageResourceGroupBuilder.WithId(1)
                                                                     .WithName(languageResourceGroupName)
                                                                     .WithApplicationId(applicationId)
                                                                     .WithApplication(application)
                                                                     .Build();

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                                                .ReturnsAsync(languageResourceGroup);

            var result = await _controller.GetAsync(applicationId, 1);

            var okObjectResult = result.Result as OkObjectResult;

            var item = Assert.IsType<LanguageResourceGroupDto>(okObjectResult.Value);
            Assert.Equal(languageResourceGroup.Id, item.Id);
            Assert.Equal(languageResourceGroup.Name, item.Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult()
        {
            var applicationId = Guid.NewGuid();
            var languageResourceGroups = new List<LanguageResourceGroup>();

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAllAsync(applicationId))
                                                .ReturnsAsync(languageResourceGroups);

            var result = await _controller.GetAllAsync(applicationId);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllItems()
        {
            var applicationId = Guid.NewGuid();

            var application = _applicationBuilder.WithId(applicationId)
                                                 .WithName("TestApplication")
                                                 .Build();

            var languageResourceGroups = new List<LanguageResourceGroup>
            {
                _languageResourceGroupBuilder.WithId(1)
                                             .WithName("TestLanguageResourceGroup")
                                             .WithApplicationId(applicationId)
                                             .WithApplication(application)
                                             .Build(),
                _languageResourceGroupBuilder.WithId(1)
                                             .WithName("TestLanguageResourceGroup")
                                             .WithApplicationId(applicationId)
                                             .WithApplication(application)
                                             .Build()
            };

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAllAsync(applicationId))
                                                .ReturnsAsync(languageResourceGroups);

            var result = await _controller.GetAllAsync(applicationId);

            var okObjectResult = result.Result as OkObjectResult;

            var items = Assert.IsAssignableFrom<IReadOnlyList<LanguageResourceGroupDto>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNotFoundResult()
        {
            LanguageResourceGroup applicationLanguage = null;

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                                                .ReturnsAsync(applicationLanguage);

            var result = await _controller.DeleteAsync(Guid.NewGuid(), 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult()
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

            _languageResourceGroupRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                                                .ReturnsAsync(languageResourceGroup);
            _languageResourceGroupRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<LanguageResourceGroup>()))
                                                .Verifiable();

            var result = await _controller.DeleteAsync(applicationId, 1);

            _languageResourceGroupRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }
    }
}

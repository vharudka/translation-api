﻿// Copyright 2020, Vladislav Harudka. All rights reserved.
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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Harudka.Translation.Api.Tests.Controllers
{
    public class LanguageControllerTests
    {
        private readonly Mock<ILanguageRepository> _languageRepositoryMock;
        private readonly LanguageBuilder _languageBuilder;
        private readonly LanguageForCreationDtoBuilder _languageForCreationDtoBuilder;
        private readonly LanguageForUpdatingDtoBuilder _languageForUpdatingDtoBuilder;
        private readonly LanguageController _controller;

        public LanguageControllerTests()
        {
            _languageRepositoryMock = new Mock<ILanguageRepository>();
            _languageBuilder = new LanguageBuilder();
            _languageForCreationDtoBuilder = new LanguageForCreationDtoBuilder();
            _languageForUpdatingDtoBuilder = new LanguageForUpdatingDtoBuilder();

            var config = new MapperConfiguration(options =>
            {
                options.CreateMap<Language, LanguageDto>();
                options.CreateMap<LanguageForCreationDto, Language>();
                options.CreateMap<LanguageForUpdatingDto, Language>();
            });
            var mapper = config.CreateMapper();

            var httpContext = new DefaultHttpContext();
            _controller = new LanguageController(_languageRepositoryMock.Object, mapper)
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
            var languages = new List<Language>();

            var result = _controller.GetOptions();

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedResponse()
        {
            var languageForCreationDto = _languageForCreationDtoBuilder.WithCode("en")
                                                                       .WithName("English")
                                                                       .Build();
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            _languageRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Language>()))
                                   .ReturnsAsync(language);

            var result = await _controller.CreateAsync(languageForCreationDto);

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedItem()
        {
            var languageForCreationDto = _languageForCreationDtoBuilder.WithCode("en")
                                                                       .WithName("English")
                                                                       .Build();
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            _languageRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Language>()))
                                   .ReturnsAsync(language);

            var result = await _controller.CreateAsync(languageForCreationDto);

            var okObjectResult = result.Result as CreatedAtRouteResult;

            var item = Assert.IsType<LanguageDto>(okObjectResult.Value);
            Assert.Equal(language.Id, item.Id);
            Assert.Equal(language.Code, item.Code);
            Assert.Equal(language.Name, item.Name);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNotFoundResult()
        {
            Language language = null;
            var languageForUpdating = _languageForUpdatingDtoBuilder.WithCode("en")
                                                                    .WithName("English")
                                                                    .Build();

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);

            var result = await _controller.UpdateAsync(0, languageForUpdating);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNoContentResult()
        {
            var languageForUpdating = _languageForUpdatingDtoBuilder.WithCode("en")
                                                                    .WithName("English")
                                                                    .Build();
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);
            _languageRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Language>()))
                                   .Verifiable();

            var result = await _controller.UpdateAsync(1, languageForUpdating);

            _languageRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFoundResult()
        {
            Language language = null;

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);

            var result = await _controller.GetAsync(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult()
        {
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);

            var result = await _controller.GetAsync(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsRightItem()
        {
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);

            var result = await _controller.GetAsync(1);

            var okObjectResult = result.Result as OkObjectResult;

            var item = Assert.IsType<LanguageDto>(okObjectResult.Value);
            Assert.Equal(language.Id, item.Id);
            Assert.Equal(language.Code, item.Code);
            Assert.Equal(language.Name, item.Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult()
        {
            var languages = new List<Language>();

            _languageRepositoryMock.Setup(x => x.GetAllAsync())
                                   .ReturnsAsync(languages);

            var result = await _controller.GetAllAsync();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllItems()
        {
            var languages = new List<Language>
            {
                _languageBuilder.WithId(1)
                                .WithCode("en")
                                .WithName("English")
                                .Build(),
                _languageBuilder.WithId(2)
                                .WithCode("pl")
                                .WithName("Polish")
                                .Build(),
            };

            _languageRepositoryMock.Setup(x => x.GetAllAsync())
                                   .ReturnsAsync(languages);

            var result = await _controller.GetAllAsync();

            var okObjectResult = result.Result as OkObjectResult;

            var items = Assert.IsAssignableFrom<IReadOnlyList<LanguageDto>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNotFoundResult()
        {
            Language language = null;

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);

            var result = await _controller.DeleteAsync(0);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            var language = _languageBuilder.WithId(1)
                                           .WithCode("en")
                                           .WithName("English")
                                           .Build();

            _languageRepositoryMock.Setup(x => x.GetAsync(It.IsAny<short>()))
                                   .ReturnsAsync(language);
            _languageRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Language>()))
                                   .Verifiable();

            var result = await _controller.DeleteAsync(1);

            _languageRepositoryMock.Verify();
            Assert.IsType<NoContentResult>(result);
        }
    }
}

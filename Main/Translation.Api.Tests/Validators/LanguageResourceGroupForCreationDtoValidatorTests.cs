// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using FluentValidation.TestHelper;
using Harudka.Translation.Api.Tests.Data;
using Harudka.Translation.Api.Tests.Data.Resources;
using Harudka.Translation.Api.Validators;
using Xunit;

namespace Harudka.Translation.Api.Tests.Validators
{
    public class LanguageResourceGroupForCreationDtoValidatorTests
    {
        protected const int NameMaxLength = 100;

        protected LanguageResourceGroupForCreationDtoBuilder _builder;
        protected LanguageResourceGroupForCreationDtoValidator _validator;

        [Fact]
        protected void NameIsValid()
        {
            var languageResourceGroupForCreationDto = _builder.WithName("English")
                                                              .Build();

            var result = _validator.TestValidate(languageResourceGroupForCreationDto);

            result.ShouldNotHaveValidationErrorFor(languageResourceGroupForCreationDto => languageResourceGroupForCreationDto.Name);
        }

        [Fact]
        public void NameIsNull()
        {
            var languageResourceGroupForCreationDto = _builder.WithName(null)
                                                              .Build();

            var result = _validator.TestValidate(languageResourceGroupForCreationDto);

            result.ShouldHaveValidationErrorFor(language => language.Name)
                  .WithErrorMessage(string.Format(ErrorMessages.Empty, nameof(languageResourceGroupForCreationDto.Name)));
        }

        [Fact]
        public void NameIsEmpty()
        {
            var languageResourceGroupForCreationDto = _builder.WithName("")
                                                              .Build();

            var result = _validator.TestValidate(languageResourceGroupForCreationDto);

            result.ShouldHaveValidationErrorFor(languageResourceGroupForCreationDto => languageResourceGroupForCreationDto.Name)
                  .WithErrorMessage(string.Format(ErrorMessages.Empty, nameof(languageResourceGroupForCreationDto.Name)));
        }

        [Fact]
        public void NameIsTooLong()
        {
            var name = "Dy98PvNqGfu84dWnqDA3dtXHRtz3KfigjdH6O3GcYsyjMbLwV4xs6tlWXQzYHlzlbiC9hLFAHef6nU5jv2zXOlZYxHGpDznphzxJ7";
            var languageResourceGroupForCreationDto = _builder.WithName(name)
                                                              .Build();

            var result = _validator.TestValidate(languageResourceGroupForCreationDto);

            result.ShouldHaveValidationErrorFor(languageResourceGroupForCreationDto => languageResourceGroupForCreationDto.Name)
                  .WithErrorMessage(string.Format(ErrorMessages.MaxLength, nameof(languageResourceGroupForCreationDto.Name), NameMaxLength, name.Length));
        }
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using FluentValidation.TestHelper;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Tests.Data;
using Harudka.Translation.Api.Tests.Data.Resources;
using Harudka.Translation.Api.Validators;
using Xunit;

namespace Harudka.Translation.Api.Tests.Validators
{
    public abstract class ApplicationBaseDtoValidatorTests<A, B, V> where A : ApplicationBaseDto
                                                                    where B : ApplicationBaseDtoBuilder<A>
                                                                    where V : ApplicationBaseDtoValidator<A>
    {
        protected const int NameMaxLength = 100;

        protected B Builder;
        protected V Validator;

        [Fact]
        protected void NameIsValid()
        {
            var language = Builder.WithName("English")
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldNotHaveValidationErrorFor(language => language.Name);
        }

        [Fact]
        public void NameIsNull()
        {
            var language = Builder.WithName(null)
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Name)
                  .WithErrorMessage(string.Format(ErrorMessages.Empty, nameof(language.Name)));
        }

        [Fact]
        public void NameIsEmpty()
        {
            var language = Builder.WithName("")
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Name)
                  .WithErrorMessage(string.Format(ErrorMessages.Empty, nameof(language.Name)));
        }

        [Fact]
        public void NameIsTooLong()
        {
            var name = "Dy98PvNqGfu84dWnqDA3dtXHRtz3KfigjdH6O3GcYsyjMbLwV4xs6tlWXQzYHlzlbiC9hLFAHef6nU5jv2zXOlZYxHGpDznphzxJ7";
            var language = Builder.WithName(name)
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Name)
                  .WithErrorMessage(string.Format(ErrorMessages.MaxLength, nameof(language.Name), NameMaxLength, name.Length));
        }
    }
}

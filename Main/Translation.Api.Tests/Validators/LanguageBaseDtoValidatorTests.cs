// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using FluentValidation.TestHelper;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Tests.Data;
using Harudka.Translation.Api.Validators;
using Xunit;

namespace Harudka.Translation.Api.Tests.Validators
{
    public abstract class LanguageBaseDtoValidatorTests<L, B, V> where L : LanguageBaseDto
                                                                 where B : LanguageBaseDtoBuilder<L>
                                                                 where V : LanguageBaseDtoValidator<L>
    {
        protected const int CodeMinLength = 2;
        protected const int CodeMaxLength = 2;
        protected const int NameMaxLength = 100;

        protected B Builder;
        protected V Validator;

        [Fact]
        public void CodeIsValid()
        {
            var language = Builder.WithCode("en")
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldNotHaveValidationErrorFor(language => language.Code);
        }

        [Fact]
        public void CodeIsNull()
        {
            var language = Builder.WithCode(null)
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Code)
                  .WithErrorMessage(string.Format(ErrorMessages.Empty, nameof(language.Code)));
        }

        [Fact]
        public void CodeIsEmpty()
        {
            var code = "";
            var language = Builder.WithCode(code)
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Code)
                  .WithErrorMessage(string.Format(ErrorMessages.Empty, nameof(language.Code)));
        }

        [Fact]
        public void CodeIsOutOfMinRange()
        {
            var code = "e";
            var language = Builder.WithCode(code)
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Code)
                  .WithErrorMessage(string.Format(ErrorMessages.Range, nameof(language.Code), CodeMinLength, CodeMaxLength, code.Length));
        }

        [Fact]
        public void CodeIsOutOfMaxRange()
        {
            var code = "eng";
            var language = Builder.WithCode("eng")
                                  .Build();

            var result = Validator.TestValidate(language);

            result.ShouldHaveValidationErrorFor(language => language.Code)
                  .WithErrorMessage(string.Format(ErrorMessages.Range, nameof(language.Code), CodeMinLength, CodeMaxLength, code.Length));
        }

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

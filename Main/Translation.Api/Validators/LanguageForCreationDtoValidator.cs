// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using FluentValidation;
using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Validators
{
    public class LanguageForCreationDtoValidator : AbstractValidator<LanguageForCreationDto>
    {
        public LanguageForCreationDtoValidator()
        {
            RuleFor(x => x.Code).NotNull()
                                .Length(2, 2);

            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);
        }
    }
}

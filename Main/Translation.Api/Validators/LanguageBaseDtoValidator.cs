// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using FluentValidation;
using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Validators
{
    public class LanguageBaseDtoValidator<T> : AbstractValidator<T> where T : LanguageBaseDto
    {
        public LanguageBaseDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty()
                                .Length(2, 2);

            RuleFor(x => x.Name).NotEmpty()
                                .MaximumLength(100);
        }
    }
}

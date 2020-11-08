// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using FluentValidation;
using Harudka.Translation.Api.Models.Requests;

namespace Harudka.Translation.Api.Validators
{
    public class CreateOrUpdateLanguageRequestValidator : AbstractValidator<CreateOrUpdateLanguageRequest>
    {
        public CreateOrUpdateLanguageRequestValidator()
        {
            RuleFor(x => x.Code).NotNull()
                                .Length(2, 2);

            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);
        }
    }
}

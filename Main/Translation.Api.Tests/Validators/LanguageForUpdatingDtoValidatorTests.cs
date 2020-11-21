// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Tests.Data;
using Harudka.Translation.Api.Validators;

namespace Harudka.Translation.Api.Tests.Validators
{
    public class LanguageForUpdatingDtoValidatorTests : LanguageBaseDtoValidatorTests<LanguageForUpdatingDto,
                                                                                      LanguageForUpdatingDtoBuilder,
                                                                                      LanguageForUpdatingDtoValidator>
    {
        public LanguageForUpdatingDtoValidatorTests()
        {
            Builder = new LanguageForUpdatingDtoBuilder();
            Validator = new LanguageForUpdatingDtoValidator();
        }
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Tests.Data;
using Harudka.Translation.Api.Validators;

namespace Harudka.Translation.Api.Tests.Validators
{
    public class ApplicationForCreationDtoValidatorTests : ApplicationBaseDtoValidatorTests<ApplicationForCreationDto,
                                                                                            ApplicationForCreationDtoBuilder,
                                                                                            ApplicationForCreationDtoValidator>
    {
        public ApplicationForCreationDtoValidatorTests()
        {
            Validator = new ApplicationForCreationDtoValidator();
            Builder = new ApplicationForCreationDtoBuilder();
        }
    }
}

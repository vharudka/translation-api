// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Tests.Data
{
    public class ApplicationForCreationDtoBuilder : ApplicationBaseDtoBuilder<ApplicationForCreationDto>
    {
        public override ApplicationForCreationDto Build()
        {
            return new ApplicationForCreationDto
            {
                Name = name
            };
        }
    }
}

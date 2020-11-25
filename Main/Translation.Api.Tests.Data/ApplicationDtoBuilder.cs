// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;
using System;

namespace Harudka.Translation.Api.Tests.Data
{
    public class ApplicationDtoBuilder : ApplicationBaseDtoBuilder<ApplicationDto>
    {
        private Guid _id;

        public override ApplicationDto Build()
        {
            return new ApplicationDto
            {
                Id = _id,
                Name = name
            };
        }

        public ApplicationDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
    }
}

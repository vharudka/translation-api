// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Tests.Data
{
    public class LanguageResourceGroupForCreationDtoBuilder
    {
        private string _name;

        public LanguageResourceGroupForCreationDto Build()
        {
            return new LanguageResourceGroupForCreationDto
            {
                Name = _name
            };
        }

        public LanguageResourceGroupForCreationDtoBuilder WithName(string value)
        {
            _name = value;
            return this;
        }
    }
}

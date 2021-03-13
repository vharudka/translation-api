// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Tests.Data
{
    public class LanguageResourceGroupDtoBuilder
    {
        private int _id;
        private string _name;

        public LanguageResourceGroupDto Build()
        {
            return new LanguageResourceGroupDto
            {
                Id = _id,
                Name = _name
            };
        }

        public LanguageResourceGroupDtoBuilder WithId(int value)
        {
            _id = value;
            return this;
        }

        public LanguageResourceGroupDtoBuilder WithLanguage(string value)
        {
            _name = value;
            return this;
        }
    }
}

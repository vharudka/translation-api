// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Tests.Data
{
    public class ApplicationLanguageForCreationDtoBuilder
    {
        private short _languageId;

        public ApplicationLanguageForCreationDto Build()
        {
            return new ApplicationLanguageForCreationDto
            {
                LanguageId = _languageId
            };
        }

        public ApplicationLanguageForCreationDtoBuilder WithLanguageId(short value)
        {
            _languageId = value;
            return this;
        }
    }
}

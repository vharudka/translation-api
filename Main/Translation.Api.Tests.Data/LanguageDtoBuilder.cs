// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Tests.Data
{
    public class LanguageDtoBuilder : LanguageBaseDtoBuilder<LanguageDto>
    {
        private short _id;

        public override LanguageDto Build()
        {
            return new LanguageDto
            {
                Id = _id,
                Code = code,
                Name = name
            };
        }

        public LanguageDtoBuilder WithCode(short id)
        {
            _id = id;
            return this;
        }
    }
}

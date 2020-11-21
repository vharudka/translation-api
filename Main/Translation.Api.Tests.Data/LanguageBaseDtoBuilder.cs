// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;

namespace Harudka.Translation.Api.Tests.Data
{
    public abstract class LanguageBaseDtoBuilder<T> where T : LanguageBaseDto
    {
        protected string code;
        protected string name;

        public abstract T Build();

        public LanguageBaseDtoBuilder<T> WithCode(string value)
        {
            code = value;
            return this;
        }

        public LanguageBaseDtoBuilder<T> WithName(string value)
        {
            name = value;
            return this;
        }
    }
}

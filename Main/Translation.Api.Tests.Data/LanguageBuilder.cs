// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;

namespace Harudka.Translation.Api.Tests.Data
{
    public class LanguageBuilder
    {
        private short _id;
        private string _code;
        private string _name;

        public Language Build()
        {
            return new Language
            {
                Id = _id,
                Code = _code,
                Name = _name
            };
        }

        public LanguageBuilder WithId(short value)
        {
            _id = value;
            return this;
        }

        public LanguageBuilder WithCode(string value)
        {
            _code = value;
            return this;
        }

        public LanguageBuilder WithName(string value)
        {
            _name = value;
            return this;
        }
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Dto;
using System;

namespace Harudka.Translation.Api.Tests.Data
{
    public class ApplicationLanguageDtoBuilder
    {
        private Guid _applicationId;
        private string _applicationName;
        private short _languageId;
        private string _languageCode;
        private string _languageName;

        public ApplicationLanguageDto Build()
        {
            return new ApplicationLanguageDto
            {
                ApplicationId = _applicationId,
                ApplicationName = _applicationName,
                LanguageId = _languageId,
                LanguageCode = _languageCode,
                LanguageName = _languageName
            };
        }

        public ApplicationLanguageDtoBuilder WithApplicationId(Guid value)
        {
            _applicationId = value;
            return this;
        }

        public ApplicationLanguageDtoBuilder WithApplicationName(string value)
        {
            _applicationName = value;
            return this;
        }

        public ApplicationLanguageDtoBuilder WithLanguageId(short value)
        {
            _languageId = value;
            return this;
        }

        public ApplicationLanguageDtoBuilder WithLanguageCode(string value)
        {
            _languageCode = value;
            return this;
        }

        public ApplicationLanguageDtoBuilder WithLanguageName(string value)
        {
            _languageName = value;
            return this;
        }
    }
}

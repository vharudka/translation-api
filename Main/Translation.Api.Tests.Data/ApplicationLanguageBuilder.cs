// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using System;

namespace Harudka.Translation.Api.Tests.Data
{
    public class ApplicationLanguageBuilder
    {
        private Guid _applicationId;
        private short _languageId;
        private Application _application;
        private Language _language;

        public ApplicationLanguage Build()
        {
            return new ApplicationLanguage
            {
                ApplicationId = _applicationId,
                Application = _application,
                LanguageId = _languageId,
                Language = _language
            };
        }

        public ApplicationLanguageBuilder WithApplicationId(Guid value)
        {
            _applicationId = value;
            return this;
        }

        public ApplicationLanguageBuilder WithApplication(Application value)
        {
            _application = value;
            return this;
        }

        public ApplicationLanguageBuilder WithLanguageId(short value)
        {
            _languageId = value;
            return this;
        }

        public ApplicationLanguageBuilder WithLanguage(Language value)
        {
            _language = value;
            return this;
        }
    }
}

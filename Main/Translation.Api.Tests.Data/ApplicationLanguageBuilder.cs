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

        public ApplicationLanguage Build()
        {
            return new ApplicationLanguage
            {
                ApplicationId = _applicationId,
                LanguageId = _languageId
            };
        }

        public ApplicationLanguageBuilder WithApplicationId(Guid value)
        {
            _applicationId = value;
            return this;
        }

        public ApplicationLanguageBuilder WithLanguageId(short value)
        {
            _languageId = value;
            return this;
        }
    }
}

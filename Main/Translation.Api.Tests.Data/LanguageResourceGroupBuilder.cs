// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using System;

namespace Harudka.Translation.Api.Tests.Data
{
    public class LanguageResourceGroupBuilder
    {
        private int _id;
        private string _name;
        private Guid _applicationId;
        private Application _application;

        public LanguageResourceGroup Build()
        {
            return new LanguageResourceGroup
            {
                Id = _id,
                Name = _name,
                ApplicationId = _applicationId,
                Application = _application
            };
        }

        public LanguageResourceGroupBuilder WithId(int value)
        {
            _id = value;
            return this;
        }

        public LanguageResourceGroupBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public LanguageResourceGroupBuilder WithApplicationId(Guid value)
        {
            _applicationId = value;
            return this;
        }

        public LanguageResourceGroupBuilder WithApplication(Application value)
        {
            _application = value;
            return this;
        }
    }
}

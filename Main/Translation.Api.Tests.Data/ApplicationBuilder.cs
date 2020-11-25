// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using System;

namespace Harudka.Translation.Api.Tests.Data
{
    public class ApplicationBuilder
    {
        private Guid _id;
        private string _name;

        public Application Build()
        {
            return new Application
            {
                Id = _id,
                Name = _name
            };
        }

        public ApplicationBuilder WithId(Guid value)
        {
            _id = value;
            return this;
        }

        public ApplicationBuilder WithName(string value)
        {
            _name = value;
            return this;
        }
    }
}

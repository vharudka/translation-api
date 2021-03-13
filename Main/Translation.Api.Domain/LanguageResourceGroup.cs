// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using System;
using System.Collections.Generic;

namespace Harudka.Translation.Api.Domain
{
    public class LanguageResourceGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}

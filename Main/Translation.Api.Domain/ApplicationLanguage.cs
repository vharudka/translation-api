// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using System;

namespace Harudka.Translation.Api.Domain
{
    public class ApplicationLanguage
    {
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
        public short LanguageId { get; set; }
        public Language Language { get; set; }
    }
}

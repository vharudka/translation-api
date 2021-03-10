// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using System;

namespace Harudka.Translation.Api.Dto
{
    public class ApplicationLanguageDto
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public short LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
    }
}

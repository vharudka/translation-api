// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Service
{
    public interface ILanguageService
    {
        Task<Language> UpdateAsync(UpdateLanguageRequest request, Language language);
        Task<Language> GetOneAsync(short id);
        Task<IReadOnlyList<Language>> GetAsync();
    }
}

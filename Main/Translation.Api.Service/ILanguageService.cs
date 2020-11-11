// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Service
{
    public interface ILanguageService
    {
        Task<Language> CreateAsync(Language language);
        Task UpdateAsync(Language language);
        Task<Language> GetAsync(short id);
        Task<IReadOnlyList<Language>> GetAllAsync();
        Task DeleteAsync(Language language);
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Repository
{
    public interface ILanguageResourceGroupRepository
    {
        Task<LanguageResourceGroup> CreateAsync(LanguageResourceGroup languageResourceGroup);
        Task<LanguageResourceGroup> GetAsync(Guid applicationId, int id);
        Task<IReadOnlyList<LanguageResourceGroup>> GetAllAsync(Guid applicationId);
        Task DeleteAsync(LanguageResourceGroup languageResourceGroup);
    }
}

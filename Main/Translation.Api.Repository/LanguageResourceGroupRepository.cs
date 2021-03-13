// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Data;
using Harudka.Translation.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Repository
{
    public class LanguageResourceGroupRepository : ILanguageResourceGroupRepository
    {
        private readonly ApplicationContext _applicationContext;

        public LanguageResourceGroupRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<LanguageResourceGroup> CreateAsync(LanguageResourceGroup languageResourceGroup)
        {
            _applicationContext.LanguageResourceGroups.Add(languageResourceGroup);
            await _applicationContext.SaveChangesAsync();

            return languageResourceGroup;
        }

        public Task<LanguageResourceGroup> GetAsync(Guid applicationId, int id)
        {
            return _applicationContext.LanguageResourceGroups.Where(x => x.Id == id &&
                                                                         x.ApplicationId == applicationId)
                                                             .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<LanguageResourceGroup>> GetAllAsync(Guid applicationId)
        {
            return await _applicationContext.LanguageResourceGroups.Where(x => x.ApplicationId == applicationId)
                                                                   .ToListAsync();
        }

        public async Task DeleteAsync(LanguageResourceGroup languageResourceGroup)
        {
            _applicationContext.LanguageResourceGroups.Remove(languageResourceGroup);
            await _applicationContext.SaveChangesAsync();
        }
    }
}

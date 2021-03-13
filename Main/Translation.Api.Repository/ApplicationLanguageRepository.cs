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
    public class ApplicationLanguageRepository : IApplicationLanguageRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationLanguageRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<ApplicationLanguage> CreateAsync(ApplicationLanguage applicationLanguage)
        {
            _applicationContext.ApplicationLanguages.Add(applicationLanguage);
            await _applicationContext.SaveChangesAsync();

            return applicationLanguage;
        }

        public Task<ApplicationLanguage> GetAsync(Guid applicationId, short languageId)
        {
            return _applicationContext.ApplicationLanguages.Where(x => x.ApplicationId == applicationId &&
                                                                       x.LanguageId == languageId)
                                                           .Include(x => x.Application)
                                                           .Include(x => x.Language)
                                                           .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<ApplicationLanguage>> GetAllAsync(Guid applicationId)
        {
            return await _applicationContext.ApplicationLanguages.Where(x => x.ApplicationId == applicationId)
                                                                 .Include(x => x.Application)
                                                                 .Include(x => x.Language)
                                                                 .ToListAsync();
        }

        public async Task DeleteAsync(ApplicationLanguage applicationLanguage)
        {
            _applicationContext.ApplicationLanguages.Remove(applicationLanguage);
            await _applicationContext.SaveChangesAsync();
        }
    }
}

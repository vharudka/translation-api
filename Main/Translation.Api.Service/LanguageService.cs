// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Data;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationContext _applicationContext;

        public LanguageService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Language> CreateAsync(CreateOrUpdateLanguageRequest request)
        {
            var language = new Language();
            language.Code = request.Code;
            language.Name = request.Name;

            _applicationContext.Languages.Add(language);
            await _applicationContext.SaveChangesAsync();

            return language;
        }

        public async Task<Language> UpdateAsync(CreateOrUpdateLanguageRequest request, Language language)
        {
            language.Code = request.Code;
            language.Name = request.Name;

            _applicationContext.Languages.Attach(language);
            await _applicationContext.SaveChangesAsync();

            return language;
        }

        public Task<Language> GetOneAsync(short id)
        {
            return _applicationContext.Languages.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Language>> GetAsync()
        {
            return await _applicationContext.Languages.ToListAsync();
        }

        public async Task<Language> DeleteAsync(Language language)
        {
            _applicationContext.Languages.Remove(language);
            await _applicationContext.SaveChangesAsync();

            return language;
        }
    }
}

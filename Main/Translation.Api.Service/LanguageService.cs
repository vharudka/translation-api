// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Data;
using Harudka.Translation.Api.Domain;
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

        public async Task<IReadOnlyList<Language>> GetAsync()
        {
            return await _applicationContext.Languages.ToListAsync();
        }
    }
}

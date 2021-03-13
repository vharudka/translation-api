// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Data;
using Harudka.Translation.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Application> CreateAsync(Application application)
        {
            _applicationContext.Applications.Add(application);
            await _applicationContext.SaveChangesAsync();

            return application;
        }

        public async Task UpdateAsync(Application application)
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Task<Application> GetAsync(Guid id)
        {
            return _applicationContext.Applications.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Application>> GetAllAsync()
        {
            return await _applicationContext.Applications.ToListAsync();
        }

        public async Task DeleteAsync(Application application)
        {
            _applicationContext.Applications.Remove(application);
            await _applicationContext.SaveChangesAsync();
        }
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Repository
{
    public interface IApplicationRepository
    {
        Task<Application> CreateAsync(Application application);
        Task UpdateAsync(Application application);
        Task<Application> GetAsync(Guid id);
        Task<IReadOnlyList<Application>> GetAllAsync();
        Task DeleteAsync(Application application);
    }
}

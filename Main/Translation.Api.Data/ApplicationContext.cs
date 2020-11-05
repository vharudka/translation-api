// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Data.Configurations;
using Harudka.Translation.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Harudka.Translation.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        }
    }
}

// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harudka.Translation.Api.Data.Configurations
{
    public class ApplicationLanguageConfiguration : IEntityTypeConfiguration<ApplicationLanguage>
    {
        public void Configure(EntityTypeBuilder<ApplicationLanguage> builder)
        {
            builder.HasKey(x => new { x.ApplicationId, x.LanguageId });

            builder.Property(x => x.LanguageId)
                   .IsRequired();

            builder.Property(x => x.ApplicationId)
                   .IsRequired();
        }
    }
}

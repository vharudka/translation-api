// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using Harudka.Translation.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harudka.Translation.Api.Data.Configurations
{
    public class LanguageResourceGroupConfiguration : IEntityTypeConfiguration<LanguageResourceGroup>
    {
        public void Configure(EntityTypeBuilder<LanguageResourceGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.ApplicationId, x.Name })
                   .IsUnique();

            builder.Property(x => x.ApplicationId)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}

using Consultas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.EntitiesConfiguration
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(b => b.Name);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);
        }

    }
}

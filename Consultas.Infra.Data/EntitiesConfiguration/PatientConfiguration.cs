using Consultas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.EntitiesConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p =>  p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(13);
    
        }
    }
}

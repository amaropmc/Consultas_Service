using Consultas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.EntitiesConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.ProfessionalNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasOne(b => b.Specialty)
                .WithMany(b => b.Doctors)
                .HasForeignKey(b => b.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

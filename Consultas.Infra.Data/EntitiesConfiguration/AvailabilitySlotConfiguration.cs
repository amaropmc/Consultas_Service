using Consultas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.EntitiesConfiguration
{
    public class AvailabilitySlotConfiguration : IEntityTypeConfiguration<AvailabilitySlot>
    {
        public void Configure(EntityTypeBuilder<AvailabilitySlot> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Doctor)
                .WithMany()
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}

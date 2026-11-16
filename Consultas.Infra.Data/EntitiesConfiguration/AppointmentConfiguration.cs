using Consultas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.EntitiesConfiguration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Patient)
                .WithMany(b => b.Appointments)
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.AvailabilitySlot)
                .WithMany()
                .HasForeignKey(b => b.AvailabilitySlotId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

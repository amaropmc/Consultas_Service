using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.Appointment
{
    public class AppointmentGetDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public required string PatientName { get; set; }

        public int AvailabilitySlotId { get; set; }

        public Status Status { get; set; }

        public DateTimeOffset StartAt { get; set; }

    }
}

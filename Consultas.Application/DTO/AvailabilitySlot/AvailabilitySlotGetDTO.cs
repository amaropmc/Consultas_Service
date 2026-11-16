using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.DTO.AvailabilitySlot
{
    public class AvailabilitySlotGetDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }

        public string? DoctorName { get; set; }

        public bool IsBooked { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
    }
}

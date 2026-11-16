using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId {  get; set; }
        public Patient Patient { get; set; } = null!;

        public int AvailabilitySlotId { get; set; }
        public AvailabilitySlot AvailabilitySlot { get; set; } = null!;

        public Status Status {  get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? CanceledAt { get; set; }

    }
}

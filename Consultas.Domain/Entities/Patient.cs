using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

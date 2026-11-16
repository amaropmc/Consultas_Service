using Consultas.Application.DTO.Appointment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.DTO.Patient
{
    public class PatientGetDTO
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public ICollection<AppointmentGetDTO>? Appointments { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.Appointment
{
    public class AppointmentPostDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Identificador do doente não é válido.")]
        public int PatientId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Identificador do horário não é válido.")]
        public int AvailabilitySlotId { get; set; }

    }
}

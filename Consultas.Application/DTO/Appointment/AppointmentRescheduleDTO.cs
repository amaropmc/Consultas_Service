using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.Appointment
{
    public class AppointmentRescheduleDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Novo horário escolhido não é válido.")]
        public required int NewAvailabilitySlotId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.AvailabilitySlot
{
    public class AvailabilitySlotPostDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Identificador do médico não é válido.")]
        public int DoctorId { get; set; }

        public bool IsBooked { get; private set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
    }
}

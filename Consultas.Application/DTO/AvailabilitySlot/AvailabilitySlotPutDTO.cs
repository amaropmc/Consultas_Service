using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.AvailabilitySlot
{
    public class AvailabilitySlotPutDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Identificador do horário não é válido.")]
        public int Id { get; set; }
        public bool IsBooked { get; set; }
    }
}

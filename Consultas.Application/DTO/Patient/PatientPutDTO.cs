using Consultas.Application.DTO.Appointment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.Patient
{
    public class PatientPutDTO
    {
        [Required(ErrorMessage = "Identificador do doente é obrigatório")]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}

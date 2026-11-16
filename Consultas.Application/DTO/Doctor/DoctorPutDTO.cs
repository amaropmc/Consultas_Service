using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.Doctor
{
    public class DoctorPutDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Identificador do médico não é válido.")]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "O nome do médico não pode ter mais do que 50 caracteres.")]
        public string? Name { get; set; }

        [MaxLength(50, ErrorMessage = "O email do médico não pode ter mais do que 50 caracteres.")]
        public string? Email { get; set; }

        [MaxLength(10, ErrorMessage = "A cédula do médico não pode ter mais do que 10 caracteres.")]
        public string? ProfessionalNumber { get; set; }
        public int? SpecialtyId { get; set; }
    }
}

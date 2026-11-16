using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Consultas.Application.DTO.Doctor
{
    public class DoctorPostDTO
    {
        [Required(ErrorMessage = "O nome do médico é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome do médico não pode ter mais do que 50 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O email do médico é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O email do médico não pode ter mais do que 50 caracteres.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A cédula do médico é obrigatória.")]
        [MaxLength(10, ErrorMessage = "A cédula do médico não pode ter mais do que 10 caracteres.")]
        public required string ProfessionalNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Identificador do médico não é válido.")]
        public int SpecialtyId { get; set; }
    }
}

using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.DTO.Doctor
{
    public class DoctorGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ProfessionalNumber { get; set; }
        public int SpecialtyId { get; set; }
    }
}

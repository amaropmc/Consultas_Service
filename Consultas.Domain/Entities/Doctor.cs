using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }
        public required string ProfessionalNumber { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

    }
}

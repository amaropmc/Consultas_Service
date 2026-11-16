using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Entities
{
    public class Specialty
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}

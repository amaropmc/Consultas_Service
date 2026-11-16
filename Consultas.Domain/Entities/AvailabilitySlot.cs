using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Entities
{
    public class AvailabilitySlot
    {
        public int Id { get; set; }
        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public bool IsBooked { get; private set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public void Free()
        {
            this.IsBooked = false;
        }

        public void Book()
        {
            if (this.IsBooked)
            {
                throw new InvalidOperationException("Este horário já não se encontra disponível");
            }

            this.IsBooked = true;

        }
    }
}

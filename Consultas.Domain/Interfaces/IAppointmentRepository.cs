using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        void AddAppointment(Appointment appointment);
        Task<Appointment?> GetAppointmentByIdAsync(int id);

        Task<PagedList<Appointment>> GetAllAppointmentsAsync(
            int pageNumber,
            int pageSize);

        Task<Appointment?> GetAppointmentWithDetails(int id);
    }
}

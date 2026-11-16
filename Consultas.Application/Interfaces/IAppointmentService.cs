using Consultas.Application.DTO.Appointment;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentGetDTO> AddAppointmentAsync(AppointmentPostDTO appointment);
        Task<AppointmentGetDTO?> GetAppointmentByIdAsync(int id);

        Task<PagedList<AppointmentGetDTO>> GetAllAppointmentsAsync(
            int pageNumber,
            int pageSize);

        Task<AppointmentGetDTO?> CancelAppointmentAsync(int id);

        Task<AppointmentGetDTO?> RescheduleAppointmentAsync(
            int id,
            AppointmentRescheduleDTO newAppointment);
    }
}

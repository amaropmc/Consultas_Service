using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using Consultas.Domain.Interfaces;
using Consultas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context) => _context = context;
        public void AddAppointment(Appointment appointment)
        {
            ArgumentNullException.ThrowIfNull(appointment);
            _context.Appointment.Add(appointment);
        }

        public async Task<PagedList<Appointment>> GetAllAppointmentsAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);

            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

            var query = _context.Appointment
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.AvailabilitySlot);

            var totalCount = await query.CountAsync();

            var appointments = await query.
                OrderBy(a => a.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Appointment>(appointments, totalCount, pageNumber, pageSize);
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointment.FindAsync(id);            
        }

        public async Task<Appointment?> GetAppointmentWithDetails(int id) => await _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.AvailabilitySlot)
                .FirstOrDefaultAsync(a => a.Id == id);
    }
}

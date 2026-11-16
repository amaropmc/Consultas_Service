using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using Consultas.Domain.Interfaces;
using Consultas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.Repositories
{
    public class AvailabilitySlotRepository : IAvailabilitySlotRepository
    {
        private readonly ApplicationDbContext _context;

        public AvailabilitySlotRepository(ApplicationDbContext context) => _context = context;

        public void AddAvailabilitySlot(AvailabilitySlot availabilitySlot)
        {
            ArgumentNullException.ThrowIfNull(availabilitySlot);
            _context.AvailabilitySlot.Add(availabilitySlot);
        }

        public async Task<PagedList<AvailabilitySlot>> GetAllAvailabilitySlotsAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

            var query = _context.AvailabilitySlot.AsNoTracking();

            var totalCount = await query.CountAsync();

            var availableSlots = await query
                .OrderBy(a => a.StartAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<AvailabilitySlot>(availableSlots, totalCount, pageNumber, pageSize);

        }

        public async Task<List<AvailabilitySlot>> GetAvailabilitySlotByDoctorAsync(int doctor)
        {
            if (doctor == 0)
                throw new ArgumentNullException("Identificador do médico não pode ser nulo.");

            return await _context.AvailabilitySlot
                .AsNoTracking()
                .Where(a => a.DoctorId == doctor)
                .ToListAsync();
        }

        public async Task<List<AvailabilitySlot>> GetAvailabilitySlotByDoctorTimeAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            if (doctorId == 0)
                throw new ArgumentNullException("Identificador do médico não pode ser nulo.");

            if (endDate < startDate)
                throw new InvalidOperationException("A data de fim do horário não pode ser menor que a data de início.");

            return await _context.AvailabilitySlot
                .AsNoTracking()
                .Where (a => a.DoctorId == doctorId
                            && a.StartAt == startDate
                            && a.EndAt == endDate)
                .ToListAsync();
        }

        public async Task<AvailabilitySlot?> GetAvailabilitySlotByIdAsync(int id)
        {
            return await _context.AvailabilitySlot.FindAsync(id)
                ?? throw new KeyNotFoundException("Não foi encontrado nenhum horário para o identificador fornecido.");
        }

        public Task<AvailabilitySlot?> GetAvailabilitySlotDetails(int id) => 
            _context.AvailabilitySlot.Include(s => s.Doctor).FirstOrDefaultAsync(s => s.Id == id);
    }
}

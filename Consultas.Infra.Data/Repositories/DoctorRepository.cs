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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddDoctorAsync(Doctor doctor)
        {
            ArgumentNullException.ThrowIfNull(doctor);
            _context.Doctor.Add(doctor);
        }

        public Task<Doctor> DeleteDoctorAsync(Doctor doctor)
        {
            ArgumentNullException.ThrowIfNull(doctor);

            _context.Doctor.Remove(doctor);
            return Task.FromResult(doctor);
        }

        public async Task<PagedList<Doctor>> GetAllDoctorsAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

            var query = _context.Doctor.AsNoTracking();

            var totalCount = await query.CountAsync();

            var doctors = await query
                .OrderBy(d => d.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Doctor>(doctors, totalCount, pageNumber, pageSize);
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int Id)
        {
            return await _context.Doctor.FindAsync(Id);
        }

        public Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {
            ArgumentNullException.ThrowIfNull(doctor);

            _context.Doctor.Update(doctor);
            return Task.FromResult(doctor);
        }
    }
}

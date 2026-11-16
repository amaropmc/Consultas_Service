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
    public class PatientRepository : IPatientRepository
    {

        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context) => _context = context;

        public void CreatePatientAsync(Patient patient)
        {
            ArgumentNullException.ThrowIfNull(patient);
            _context.Patient.Add(patient);
        }

        public Task<Patient> DeletePatientAsync(Patient patient)
        {
            ArgumentNullException.ThrowIfNull(patient);
            _context.Patient.Remove(patient);
            return Task.FromResult(patient);
        }

        public async Task<PagedList<Patient>> GetAllPatientsAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

            var query = _context.Patient.AsNoTracking();

            var totalCount = await query.CountAsync();

            var patients = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Patient>(patients, totalCount, pageNumber, pageSize);
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patient.FindAsync(id);
        }

        public Task<Patient> UpdatePatientAsync(Patient patient)
        {
            ArgumentNullException.ThrowIfNull(patient);

            _context.Patient.Update(patient);
            return Task.FromResult(patient);
        }
    }
}

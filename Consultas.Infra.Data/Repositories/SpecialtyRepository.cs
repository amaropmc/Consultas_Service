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
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyRepository(ApplicationDbContext context) => _context = context;
        public void CreateSpecialtyAsync(Specialty specialty)
        {
            ArgumentNullException.ThrowIfNull(specialty);
            _context.Specialty.Add(specialty);
        }

        public Task<Specialty> DeleteSpecialtyAsync(Specialty specialty)
        {
            ArgumentNullException.ThrowIfNull(specialty);
            _context.Specialty.Remove(specialty);
            return Task.FromResult(specialty);
        }

        public async Task<PagedList<Specialty>> GetAllSpecialtiesAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

            var query = _context.Specialty.AsNoTracking();

            var totalCount = await query.CountAsync();

            var specialties = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Specialty>(specialties, totalCount, pageNumber, pageSize);
        }

        public async Task<Specialty?> GetSpecialtyByIdAsync(int id)
        {
            return await _context.Specialty.FindAsync(id);
        }

        public Task<Specialty> UpdateSpecialtyAsync(Specialty specialty)
        {
            ArgumentNullException.ThrowIfNull(specialty);
            _context.Specialty.Update(specialty);
            return Task.FromResult(specialty);
        }
    }
}

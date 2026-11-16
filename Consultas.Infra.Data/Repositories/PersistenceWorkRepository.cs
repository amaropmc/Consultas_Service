using Consultas.Domain.Interfaces;
using Consultas.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Data.Repositories
{
    public class PersistenceWorkRepository : IPersistenceWorkRepository
    {
        private readonly ApplicationDbContext _context;

        public PersistenceWorkRepository(ApplicationDbContext context) => _context = context;
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

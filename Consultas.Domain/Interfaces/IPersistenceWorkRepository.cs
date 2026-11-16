using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Interfaces
{
    public interface IPersistenceWorkRepository
    {
        Task<int> SaveChangesAsync();
    }
}

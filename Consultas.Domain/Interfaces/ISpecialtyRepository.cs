using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<Specialty?> GetSpecialtyByIdAsync(int id);

        Task<PagedList<Specialty>> GetAllSpecialtiesAsync(
            int pageNumber,
            int pageSize);

        void CreateSpecialtyAsync(Specialty specialty);

        Task<Specialty> UpdateSpecialtyAsync(Specialty specialty);
        
        Task<Specialty> DeleteSpecialtyAsync(Specialty specialty);
    }
}

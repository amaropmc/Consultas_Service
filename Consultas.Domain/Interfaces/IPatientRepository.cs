using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetPatientByIdAsync(int id);

        Task<PagedList<Patient>> GetAllPatientsAsync(
            int pageNumber,
            int pageSize);

        void CreatePatientAsync(Patient patient);

        Task<Patient> UpdatePatientAsync(Patient patient);

        Task<Patient> DeletePatientAsync(Patient patient);
    }
}

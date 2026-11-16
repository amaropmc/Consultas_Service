using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetDoctorByIdAsync(int Id);

        Task<PagedList<Doctor>> GetAllDoctorsAsync(
            int pageNumber,
            int pageSize);

        void AddDoctorAsync(Doctor doctor);

        Task<Doctor> UpdateDoctorAsync(Doctor doctor);

        Task<Doctor> DeleteDoctorAsync(Doctor doctor);

    }
}

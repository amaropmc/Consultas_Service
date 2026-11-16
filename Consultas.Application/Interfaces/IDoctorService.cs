using Consultas.Application.DTO.Doctor;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorGetDTO?> GetDoctorByIdAsync(int id);

        Task<PagedList<DoctorGetDTO>> GetAllDoctorsAsync(
            int pageNumber,
            int pageSize);

        Task<DoctorGetDTO> AddDoctorAsync(DoctorPostDTO doctor);

        Task<DoctorGetDTO?> UpdateDoctorAsync(DoctorPutDTO doctor);

        Task<DoctorGetDTO?> DeleteDoctorAsync(DoctorPutDTO doctor);
    }
}

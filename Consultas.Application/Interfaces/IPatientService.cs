using Consultas.Application.DTO.Patient;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientGetDTO?> GetPatientByIdAsync(int id);

        Task<PagedList<PatientGetDTO>> GetAllPatientsAsync(
            int pageNumber,
            int pageSize);

        Task<PatientGetDTO> CreatePatientAsync(PatientPostDTO patient);

        Task<PatientGetDTO> UpdatePatientAsync(PatientPutDTO patient);

        Task<PatientGetDTO> DeletePatientAsync(PatientPutDTO patient);
    }
}

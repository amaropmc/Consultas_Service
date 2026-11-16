using Consultas.Application.DTO.Patient;
using Consultas.Application.Interfaces;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using Consultas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPersistenceWorkRepository _persistenceRepository;

        public PatientService(IPatientRepository patientRepository,
            IPersistenceWorkRepository persistenceRepository)
        {
            _patientRepository = patientRepository;
            _persistenceRepository = persistenceRepository;
        }
        public async Task<PatientGetDTO> CreatePatientAsync(PatientPostDTO patient)
        {
            var newPatient = new Patient
            {
                Name = patient.Name,
                Email = patient.Email,
                BirthDate = patient.BirthDate,
                PhoneNumber = patient.PhoneNumber,
            };

            _patientRepository.CreatePatientAsync(newPatient);
            await _persistenceRepository.SaveChangesAsync();

            return new PatientGetDTO
            {
                Id = newPatient.Id,
                Name = patient.Name,
                Email = patient.Email,
                BirthDate = patient.BirthDate,
                PhoneNumber = patient.PhoneNumber,
            };
        }

        public Task<PatientGetDTO> DeletePatientAsync(PatientPutDTO patient)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PatientGetDTO>> GetAllPatientsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<PatientGetDTO?> GetPatientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PatientGetDTO> UpdatePatientAsync(PatientPutDTO patient)
        {
            throw new NotImplementedException();
        }
    }
}

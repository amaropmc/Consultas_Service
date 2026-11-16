using Consultas.Application.DTO.Doctor;
using Consultas.Application.Interfaces;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using Consultas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPersistenceWorkRepository _persistenceWorkRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IAvailabilitySlotRepository _slotRepository;

        public DoctorService (IDoctorRepository doctorRepository,
            IPersistenceWorkRepository persistenceWorkRepository,
            ISpecialtyRepository specialtyRepository,
            IAvailabilitySlotRepository slotRepository)
        {
            _doctorRepository = doctorRepository;
            _persistenceWorkRepository = persistenceWorkRepository;
            _specialtyRepository = specialtyRepository;
            _slotRepository = slotRepository;
        }

        public async Task<DoctorGetDTO> AddDoctorAsync(DoctorPostDTO doctor)
        {
            ArgumentNullException.ThrowIfNull(doctor);

            if (doctor.Name == null || doctor.Email == null || doctor.ProfessionalNumber == null)
                ArgumentNullException.ThrowIfNull("Tem de disponibilizar a informação nome, email e cédulo do médico.");

            var doctorSpecialty = await _specialtyRepository.GetSpecialtyByIdAsync(doctor.SpecialtyId)
                ?? throw new KeyNotFoundException("Não foi encontrada especialidade do identificador disponibilizado.");

            var doctorToAdd = new Doctor
            {
                Name = doctor.Name,
                Email = doctor.Email,
                ProfessionalNumber = doctor.ProfessionalNumber,
                SpecialtyId = doctor.SpecialtyId,
                Specialty = doctorSpecialty
            };

            _doctorRepository.AddDoctorAsync(doctorToAdd);
            await _persistenceWorkRepository.SaveChangesAsync();

            return new DoctorGetDTO
            {
                Name = doctorToAdd.Name,
                Email = doctorToAdd.Email,
                ProfessionalNumber = doctorToAdd.ProfessionalNumber,
                SpecialtyId = doctorToAdd.SpecialtyId
            };
        }

        public async Task<DoctorGetDTO?> DeleteDoctorAsync(DoctorPutDTO doctor)
        {
            var doctorToDelete = await _doctorRepository.GetDoctorByIdAsync(doctor.Id)
                ?? throw new KeyNotFoundException("Não foi encontrado médico associado ao identificador disponibilizado.");

            var deletedDoctor = await _doctorRepository.DeleteDoctorAsync(doctorToDelete);

            await _persistenceWorkRepository.SaveChangesAsync();

            return new DoctorGetDTO
            {
                Id = deletedDoctor.Id,
                Name = deletedDoctor.Name,
                Email = deletedDoctor.Email,
                ProfessionalNumber = deletedDoctor.ProfessionalNumber,
                SpecialtyId = deletedDoctor.SpecialtyId
            };
        }

        public async Task<PagedList<DoctorGetDTO>> GetAllDoctorsAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

            var doctors = await _doctorRepository.GetAllDoctorsAsync(pageNumber, pageSize);

            var doctorsDTOList = doctors.Items
                .Select(d => new DoctorGetDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Email = d.Email,
                    ProfessionalNumber = d.ProfessionalNumber,
                    SpecialtyId = d.SpecialtyId
                })
                .ToList();

            return new PagedList<DoctorGetDTO> 
            (
                doctorsDTOList,
                doctors.TotalCount,
                pageNumber,
                pageSize
            );
        }

        public async Task<DoctorGetDTO?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id)
                ?? throw new KeyNotFoundException("Não foi encontrado médico associado ao identificador disponibilizado.");

            return new DoctorGetDTO
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Email = doctor.Email,
                ProfessionalNumber = doctor.ProfessionalNumber,
                SpecialtyId = doctor.SpecialtyId
            };
        }

        public async Task<DoctorGetDTO?> UpdateDoctorAsync(DoctorPutDTO doctor)
        {
            var currentDoctor = await _doctorRepository.GetDoctorByIdAsync(doctor.Id)
                ?? throw new KeyNotFoundException("Não foi encontrado médico associado ao identificador disponibilizado.");

            if (doctor.Name is not null)
            {
                currentDoctor.Name = doctor.Name;
            }

            if (doctor.Email is not null)
            {
                currentDoctor.Email = doctor.Email;
            }

            if (doctor.ProfessionalNumber is not null)
            {
                currentDoctor.ProfessionalNumber = doctor.ProfessionalNumber;
            }

            if (doctor.SpecialtyId is int specialtyId)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(specialtyId);

                var newSpecialty = await _specialtyRepository.GetSpecialtyByIdAsync(specialtyId)
                                    ?? throw new KeyNotFoundException("Não foi encontrada especialdiade associada ao identificador disponibilizado.");

                currentDoctor.Specialty = newSpecialty;
            }


            currentDoctor = await _doctorRepository.UpdateDoctorAsync(currentDoctor);
            await _persistenceWorkRepository.SaveChangesAsync();

            return new DoctorGetDTO
            {
                Id = currentDoctor.Id,
                Name = currentDoctor.Name,
                Email = currentDoctor.Email,
                ProfessionalNumber = currentDoctor.ProfessionalNumber,
                SpecialtyId = currentDoctor.SpecialtyId,
            };
        }
    }
}

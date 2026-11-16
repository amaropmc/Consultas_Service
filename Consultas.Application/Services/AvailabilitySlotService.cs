using Consultas.Application.DTO.AvailabilitySlot;
using Consultas.Application.Interfaces;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using Consultas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Consultas.Application.Services
{
    public class AvailabilitySlotService : IAvailabilitySlotService
    {
        private readonly IAvailabilitySlotRepository _availabilitySlotRepository;

        private readonly IDoctorRepository _doctorRepository; 
        private readonly IPersistenceWorkRepository _persistenceWorkRepository;

        public AvailabilitySlotService(IAvailabilitySlotRepository availabilitySlotRepository,
            IPersistenceWorkRepository persistenceWorkRepository,
            IDoctorRepository doctorRepository)
        {
            _availabilitySlotRepository = availabilitySlotRepository;
            _persistenceWorkRepository = persistenceWorkRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<AvailabilitySlotGetDTO> AddAvailabilitySlot(AvailabilitySlotPostDTO availabilitySlot)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(availabilitySlot.DoctorId)
                ?? throw new KeyNotFoundException("Não foi encontrado nenhum médico com o identificador fornecido.");

            var existsSlot = await _availabilitySlotRepository.GetAvailabilitySlotByDoctorTimeAsync(availabilitySlot.DoctorId,
                availabilitySlot.StartAt,
                availabilitySlot.EndAt);

            if (existsSlot.Count != 0)
                throw new InvalidOperationException("Já existe o horário que está a tentar marcar para o médico definido.");

            var newSlot = new AvailabilitySlot
            {
                DoctorId = availabilitySlot.DoctorId,
                Doctor = doctor,
                StartAt = availabilitySlot.StartAt,
                EndAt = availabilitySlot.EndAt
            };

            if (availabilitySlot.IsBooked)
            {
                newSlot.Book();
            }

            _availabilitySlotRepository.AddAvailabilitySlot(newSlot);
            await _persistenceWorkRepository.SaveChangesAsync();

            return new AvailabilitySlotGetDTO
            {
                Id = newSlot.Id,
                DoctorId = newSlot.DoctorId,
                DoctorName = newSlot.Doctor.Name,
                IsBooked = newSlot.IsBooked,
                StartAt = newSlot.StartAt,
                EndAt = newSlot.EndAt,
            };               
        }

        public async Task<PagedList<AvailabilitySlotGetDTO>> GetAllAvailabilitySlotsAsync(int pageNumber, int pageSize)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber,1);
            ArgumentOutOfRangeException.ThrowIfLessThan(pageSize,1);

            var slots = await _availabilitySlotRepository.GetAllAvailabilitySlotsAsync(pageNumber, pageSize);

            var result = slots.Items
                .Select(s => new AvailabilitySlotGetDTO
                    {
                        Id = s.Id,
                        DoctorId = s.DoctorId,
                        DoctorName = s.Doctor.Name,
                        IsBooked = s.IsBooked,
                        StartAt = s.StartAt,
                        EndAt = s.EndAt
                    })
                .ToList();

            return new PagedList<AvailabilitySlotGetDTO>
            (
                result,
                slots.TotalCount,
                pageNumber,
                pageSize
            );
        }

        public async Task<AvailabilitySlotGetDTO?> GetAvailabilitySlotByIdAsync(int id)
        {
            var slot = await _availabilitySlotRepository.GetAvailabilitySlotDetails(id)
                ?? throw new KeyNotFoundException("Não foi encontrado nenhum horário para o identificador fornecido.");

            return new AvailabilitySlotGetDTO
            {
                Id = slot.Id,
                DoctorId = slot.DoctorId,
                DoctorName = slot.Doctor.Name,
                IsBooked = slot.IsBooked,
                StartAt = slot.StartAt,
                EndAt = slot.EndAt
            };
        }

        public async Task<AvailabilitySlotGetDTO> UpdateAvailabilitySlot(AvailabilitySlotPutDTO availabilitySlot)
        {
            var slot = await _availabilitySlotRepository.GetAvailabilitySlotByIdAsync(availabilitySlot.Id)
                ?? throw new KeyNotFoundException("Não foi encontrado nenhum horário com o identificador fornecido.");

            if (availabilitySlot.IsBooked)
            {
                slot.Book();
            }

            await _persistenceWorkRepository.SaveChangesAsync();

            return new AvailabilitySlotGetDTO
            {
                Id = slot.Id,
                DoctorId = slot.DoctorId,
                DoctorName = slot.Doctor.Name,
                IsBooked = slot.IsBooked,
                StartAt = slot.StartAt,
                EndAt = slot.EndAt
            };
        }
    }
}

using Consultas.Application.DTO.Appointment;
using Consultas.Application.Interfaces;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using Consultas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAvailabilitySlotRepository _slotRepository;
        private readonly IPersistenceWorkRepository _persistenceRepository;


        public AppointmentService(IAppointmentRepository appointmentRepository, 
            IPatientRepository patientRepository, 
            IAvailabilitySlotRepository slotRepository, 
            IPersistenceWorkRepository persistenceRepository  )
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _slotRepository = slotRepository;
            _persistenceRepository = persistenceRepository;
        }

        public async Task<AppointmentGetDTO> AddAppointmentAsync(AppointmentPostDTO appointmentPostDTO)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(appointmentPostDTO.PatientId) 
                ?? throw new KeyNotFoundException("Não foi encontrado paciente para o identificador fornecido.");
            
            var slot = await _slotRepository.GetAvailabilitySlotByIdAsync(appointmentPostDTO.AvailabilitySlotId) 
                ?? throw new KeyNotFoundException("não foi encontrado horário para o identificador fornecido.");

            slot.Book();

            var newAppointment = new Appointment
            {
                PatientId = appointmentPostDTO.PatientId,
                Patient = patient,
                AvailabilitySlotId = appointmentPostDTO.AvailabilitySlotId,
                AvailabilitySlot = slot,
                Status = Status.Scheduled,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _appointmentRepository.AddAppointment(newAppointment);

            await _persistenceRepository.SaveChangesAsync();

            return new AppointmentGetDTO
            {
                Id = newAppointment.Id,
                PatientId = patient.Id,
                PatientName = patient.Name,
                AvailabilitySlotId = newAppointment.AvailabilitySlotId,
                Status = newAppointment.Status,
                StartAt = slot.StartAt
            };
        }

        public async Task<AppointmentGetDTO?> CancelAppointmentAsync(int id)
        {
            var appointmentToCancel = await _appointmentRepository.GetAppointmentWithDetails(id)
                ?? throw new KeyNotFoundException("Não foi encontrada nenhuma consulta com o identificador indicado.");

            if (appointmentToCancel.Status == Status.Cancelled)
                throw new InvalidOperationException("Consulta que está a tentar cancelar já se encontra cancelada.");

            if (appointmentToCancel.Status == Status.Completed)
                throw new InvalidOperationException("Não pode remarcar uma consulta que já se encontra efetuada.");

            appointmentToCancel.Status = Status.Cancelled;
            appointmentToCancel.CanceledAt = DateTimeOffset.UtcNow;
            appointmentToCancel.AvailabilitySlot.Free();

            await _persistenceRepository.SaveChangesAsync();

            return new AppointmentGetDTO
            {
                Id = appointmentToCancel.Id,
                PatientId = appointmentToCancel.PatientId,
                PatientName = appointmentToCancel.Patient.Name,
                AvailabilitySlotId = appointmentToCancel.AvailabilitySlotId,
                Status = appointmentToCancel.Status,
                StartAt = appointmentToCancel.AvailabilitySlot.StartAt
            };
        }

        public async Task<PagedList<AppointmentGetDTO>> GetAllAppointmentsAsync(int pageNumber, int pageSize)
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync(pageNumber, pageSize);

            var result = appointments.Items
                .Select(a => new AppointmentGetDTO
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name,
                    AvailabilitySlotId = a.AvailabilitySlotId,
                    Status = a.Status,
                    StartAt = a.AvailabilitySlot.StartAt
                })
                .ToList();

            return new PagedList<AppointmentGetDTO> (
                result,
                appointments.TotalCount,
                pageNumber,
                pageSize);
        }

        public async Task<AppointmentGetDTO?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithDetails(id)
                ?? throw new KeyNotFoundException("Não foi encontrada nenhuma consulta para o id fornecido.");

            return new AppointmentGetDTO
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient.Name,
                AvailabilitySlotId = appointment.AvailabilitySlotId,
                Status = appointment.Status,
                StartAt = appointment.AvailabilitySlot.StartAt
            };
        }

        public async Task<AppointmentGetDTO?> RescheduleAppointmentAsync(int id, AppointmentRescheduleDTO newAppointment)
        {
            var oldAppointment = await _appointmentRepository.GetAppointmentWithDetails(id)
                ?? throw new KeyNotFoundException("Não foi encontrada nenhuma consulta com o identificador fornecido.");

            oldAppointment.Status = Status.Cancelled;
            oldAppointment.AvailabilitySlot.Free();
            oldAppointment.CanceledAt = DateTimeOffset.UtcNow;
            oldAppointment.UpdatedAt = DateTimeOffset.UtcNow;

            var newSlot = await _slotRepository.GetAvailabilitySlotByIdAsync(newAppointment.NewAvailabilitySlotId)
                ?? throw new KeyNotFoundException("Não foi encontrado nenhum horário com o identificador fornecido.");

            if (newSlot.StartAt < DateTime.UtcNow)
                throw new InvalidOperationException("A data da consulta a efetuar não pode ser inferior à data de hoje.");

            newSlot.Book();
            
            var rescheduledAppointment = new Appointment
            {
                PatientId = oldAppointment.PatientId,
                Patient = oldAppointment.Patient,
                AvailabilitySlot = newSlot,
                AvailabilitySlotId = newSlot.Id,
                Status = Status.Scheduled,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _appointmentRepository.AddAppointment(rescheduledAppointment);
            await _persistenceRepository.SaveChangesAsync();

            return new AppointmentGetDTO
            {
                Id = rescheduledAppointment.Id,
                PatientId = rescheduledAppointment.PatientId,
                PatientName = rescheduledAppointment.Patient.Name,
                AvailabilitySlotId = newSlot.Id,
                Status = rescheduledAppointment.Status,
                StartAt = newSlot.StartAt
            };
        }
    }
}

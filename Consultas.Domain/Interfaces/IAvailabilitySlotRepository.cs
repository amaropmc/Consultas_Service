using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Interfaces
{
    public interface IAvailabilitySlotRepository
    {
        Task<AvailabilitySlot?> GetAvailabilitySlotByIdAsync(int id);

        Task<PagedList<AvailabilitySlot>> GetAllAvailabilitySlotsAsync(
            int pageNumber,
            int pageSize);

        Task<List<AvailabilitySlot>> GetAvailabilitySlotByDoctorTimeAsync(int doctor,
            DateTime startDate,
            DateTime endDate);

        Task<List<AvailabilitySlot>> GetAvailabilitySlotByDoctorAsync(int doctorId);

        void AddAvailabilitySlot (AvailabilitySlot availabilitySlot);
        
        Task<AvailabilitySlot?> GetAvailabilitySlotDetails(int id);
    }
}

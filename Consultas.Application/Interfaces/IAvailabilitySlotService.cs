using Consultas.Application.DTO.AvailabilitySlot;
using Consultas.Domain.Common;
using Consultas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Application.Interfaces
{
    public interface IAvailabilitySlotService
    {
        Task<AvailabilitySlotGetDTO?> GetAvailabilitySlotByIdAsync(int id);

        Task<PagedList<AvailabilitySlotGetDTO>> GetAllAvailabilitySlotsAsync(
            int pageNumber,
            int pageSize);

        Task<AvailabilitySlotGetDTO> AddAvailabilitySlot(AvailabilitySlotPostDTO availabilitySlot);

        Task<AvailabilitySlotGetDTO> UpdateAvailabilitySlot(AvailabilitySlotPutDTO availabilitySlot);
    }
}

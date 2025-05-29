using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.TrustedContact;

namespace AlertarApp.Application.Interfaces
{
    public interface ITrustedContactService
    {
        Task<TrustedContactDto> GetTrustedContactByIdAsync(Guid id);
        Task<IEnumerable<TrustedContactDto>> GetTrustedContactsByUserIdAsync(Guid userId);
        Task<TrustedContactDto> CreateTrustedContactAsync(Guid userId, CreateTrustedContactDto createTrustedContactDto);
        Task<TrustedContactDto> UpdateTrustedContactAsync(Guid userId, UpdateTrustedContactDto updateTrustedContactDto);
        Task<bool> DeleteTrustedContactAsync(Guid userId, Guid id);
        Task<bool> ActivateTrustedContactAsync(Guid userId, Guid id);
        Task<bool> DeactivateTrustedContactAsync(Guid userId, Guid id);
    }
}

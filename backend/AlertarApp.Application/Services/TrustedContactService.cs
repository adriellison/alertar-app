using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.TrustedContact;
using AlertarApp.Application.Interfaces;
using AlertarApp.Domain.Entities;
using AlertarApp.Domain.Interfaces;

namespace AlertarApp.Application.Services
{
    public class TrustedContactService : ITrustedContactService
    {
        private readonly ITrustedContactRepository _trustedContactRepository;

        public TrustedContactService(ITrustedContactRepository trustedContactRepository)
        {
            _trustedContactRepository = trustedContactRepository;
        }

        public async Task<TrustedContactDto> GetTrustedContactByIdAsync(Guid id)
        {
            var contact = await _trustedContactRepository.GetByIdAsync(id);
            return contact != null ? MapToDto(contact) : null;
        }

        public async Task<IEnumerable<TrustedContactDto>> GetTrustedContactsByUserIdAsync(Guid userId)
        {
            var contacts = await _trustedContactRepository.GetByUserIdAsync(userId);
            return contacts.Select(MapToDto);
        }

        public async Task<TrustedContactDto> CreateTrustedContactAsync(Guid userId, CreateTrustedContactDto createTrustedContactDto)
        {
            var contact = new TrustedContact(
                userId,
                createTrustedContactDto.Name,
                createTrustedContactDto.Phone,
                createTrustedContactDto.Email,
                createTrustedContactDto.SendSms,
                createTrustedContactDto.SendEmail);

            await _trustedContactRepository.AddAsync(contact);
            return MapToDto(contact);
        }

        public async Task<TrustedContactDto> UpdateTrustedContactAsync(Guid userId, UpdateTrustedContactDto updateTrustedContactDto)
        {
            var contact = await _trustedContactRepository.GetByIdAsync(updateTrustedContactDto.Id);
            if (contact == null || contact.UserId != userId)
                return null;

            contact.Update(
                updateTrustedContactDto.Name,
                updateTrustedContactDto.Phone,
                updateTrustedContactDto.Email,
                updateTrustedContactDto.SendSms,
                updateTrustedContactDto.SendEmail);

            await _trustedContactRepository.UpdateAsync(contact);
            return MapToDto(contact);
        }

        public async Task<bool> DeleteTrustedContactAsync(Guid userId, Guid id)
        {
            var contact = await _trustedContactRepository.GetByIdAsync(id);
            if (contact == null || contact.UserId != userId)
                return false;

            return await _trustedContactRepository.DeleteAsync(id);
        }

        public async Task<bool> ActivateTrustedContactAsync(Guid userId, Guid id)
        {
            var contact = await _trustedContactRepository.GetByIdAsync(id);
            if (contact == null || contact.UserId != userId)
                return false;

            contact.Activate();
            await _trustedContactRepository.UpdateAsync(contact);
            return true;
        }

        public async Task<bool> DeactivateTrustedContactAsync(Guid userId, Guid id)
        {
            var contact = await _trustedContactRepository.GetByIdAsync(id);
            if (contact == null || contact.UserId != userId)
                return false;

            contact.Deactivate();
            await _trustedContactRepository.UpdateAsync(contact);
            return true;
        }

        private static TrustedContactDto MapToDto(TrustedContact contact)
        {
            return new TrustedContactDto
            {
                Id = contact.Id,
                UserId = contact.UserId,
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                SendSms = contact.SendSms,
                SendEmail = contact.SendEmail,
                CreatedAt = contact.CreatedAt,
                IsActive = contact.IsActive
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.Alert;
using AlertarApp.Application.Interfaces;
using AlertarApp.Domain.Entities;
using AlertarApp.Domain.Interfaces;

namespace AlertarApp.Application.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;

        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<AlertDto> GetAlertByIdAsync(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            return alert != null ? MapToDto(alert) : null;
        }

        public async Task<IEnumerable<AlertDto>> GetUserAlertsAsync(Guid userId)
        {
            var alerts = await _alertRepository.GetByUserIdAsync(userId);
            return alerts.Select(MapToDto);
        }

        public async Task<IEnumerable<AlertDto>> GetNearbyAlertsAsync(double latitude, double longitude, double radiusInKm = 5.0)
        {
            var alerts = await _alertRepository.GetNearbyAlertsAsync(latitude, longitude, radiusInKm);
            return alerts.Select(MapToDto);
        }

        public async Task<AlertDto> CreateAlertAsync(Guid userId, CreateAlertDto createAlertDto)
        {
            var alert = new Alert(
                userId,
                createAlertDto.Title,
                createAlertDto.Description,
                createAlertDto.Latitude,
                createAlertDto.Longitude);

            await _alertRepository.AddAsync(alert);
            return MapToDto(alert);
        }

        public async Task<bool> ResolveAlertAsync(Guid id, Guid userId)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null || alert.UserId != userId)
                return false;

            alert.Resolve();
            await _alertRepository.UpdateAsync(alert);
            return true;
        }

        public async Task<bool> CancelAlertAsync(Guid id, Guid userId)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null || alert.UserId != userId)
                return false;

            alert.Cancel();
            await _alertRepository.UpdateAsync(alert);
            return true;
        }

        private static AlertDto MapToDto(Alert alert)
        {
            return new AlertDto
            {
                Id = alert.Id,
                UserId = alert.UserId,
                Title = alert.Title,
                Description = alert.Description,
                Latitude = alert.Latitude,
                Longitude = alert.Longitude,
                CreatedAt = alert.CreatedAt,
                Status = alert.Status.ToString()
            };
        }
    }
}

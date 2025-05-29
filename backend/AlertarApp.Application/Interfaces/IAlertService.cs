using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.Alert;

namespace AlertarApp.Application.Interfaces
{
    public interface IAlertService
    {
        Task<AlertDto> GetAlertByIdAsync(Guid id);
        Task<IEnumerable<AlertDto>> GetUserAlertsAsync(Guid userId);
        Task<IEnumerable<AlertDto>> GetNearbyAlertsAsync(double latitude, double longitude, double radiusInKm = 5.0);
        Task<AlertDto> CreateAlertAsync(Guid userId, CreateAlertDto createAlertDto);
        Task<bool> ResolveAlertAsync(Guid id, Guid userId);
        Task<bool> CancelAlertAsync(Guid id, Guid userId);
    }
}

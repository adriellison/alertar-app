using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertarApp.Domain.Entities;

namespace AlertarApp.Domain.Interfaces
{
    public interface IAlertRepository
    {
        Task<Alert> GetByIdAsync(Guid id);
        Task<IEnumerable<Alert>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Alert>> GetNearbyAlertsAsync(double latitude, double longitude, double radiusInKm);
        Task AddAsync(Alert alert);
        Task UpdateAsync(Alert alert);
        Task<IEnumerable<Alert>> GetActiveAlertsAsync();
    }
}

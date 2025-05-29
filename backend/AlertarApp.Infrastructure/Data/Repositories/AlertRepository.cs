using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlertarApp.Domain.Entities;
using AlertarApp.Domain.Enums;
using AlertarApp.Domain.Interfaces;
using AlertarApp.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AlertarApp.Infrastructure.Data.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly AlertarDbContext _context;

        public AlertRepository(AlertarDbContext context)
        {
            _context = context;
        }

        public async Task<Alert> GetByIdAsync(Guid id)
        {
            return await _context.Alerts.FindAsync(id);
        }

        public async Task<IEnumerable<Alert>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Alerts
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alert>> GetNearbyAlertsAsync(double latitude, double longitude, double radiusInKm)
        {
            // Implementação simplificada usando distância euclidiana
            // Em produção, considere usar funções espaciais do SQL
            const double earthRadiusKm = 6371.0;
            
            // Buscar todos alertas ativos e filtrá-los pelo código
            var activeAlerts = await _context.Alerts
                .Where(a => a.Status == AlertStatus.Active)
                .ToListAsync();
                
            return activeAlerts.Where(a => 
            {
                double dLat = DegreesToRadians(a.Latitude - latitude);
                double dLon = DegreesToRadians(a.Longitude - longitude);
                double lat1 = DegreesToRadians(latitude);
                double lat2 = DegreesToRadians(a.Latitude);

                double haversine = Math.Sin(dLat/2) * Math.Sin(dLat/2) +
                                   Math.Sin(dLon/2) * Math.Sin(dLon/2) * 
                                   Math.Cos(lat1) * Math.Cos(lat2);
                double c = 2 * Math.Asin(Math.Sqrt(haversine));
                double distance = earthRadiusKm * c;
                
                return distance <= radiusInKm;
            });
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public async Task AddAsync(Alert alert)
        {
            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Alert alert)
        {
            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Alert>> GetActiveAlertsAsync()
        {
            return await _context.Alerts
                .Where(a => a.Status == AlertStatus.Active)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}

using System;
using AlertarApp.Domain.Enums;

namespace AlertarApp.Domain.Entities
{
    public class Alert
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public AlertStatus Status { get; private set; }

        // Construtor privado para EF Core
        private Alert() { }

        public Alert(Guid userId, string title, string description, double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            CreatedAt = DateTime.UtcNow;
            Status = AlertStatus.Active;
            
            Validate();
        }

        private void Validate()
        {
            if (UserId == Guid.Empty)
                throw new ArgumentException("UserId é obrigatório");
            
            if (string.IsNullOrWhiteSpace(Title))
                throw new ArgumentException("Título do alerta é obrigatório");
            
            // Adicione validações adicionais conforme necessário
        }

        public void Resolve()
        {
            Status = AlertStatus.Resolved;
        }

        public void Cancel()
        {
            Status = AlertStatus.Cancelled;
        }
    }
}

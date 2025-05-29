using System;

namespace AlertarApp.Application.DTOs.Alert
{
    public class AlertDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}

using System;

namespace AlertarApp.Application.DTOs.Alert
{
    public class CreateAlertDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

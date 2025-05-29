using System;

namespace AlertarApp.Application.DTOs.Alert
{
    public class UpdateAlertDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

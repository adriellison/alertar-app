using System;

namespace AlertarApp.Application.DTOs.TrustedContact
{
    public class UpdateTrustedContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool SendSms { get; set; }
        public bool SendEmail { get; set; }
    }
}

using System;

namespace AlertarApp.Application.DTOs.TrustedContact
{
    public class TrustedContactDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool SendSms { get; set; }
        public bool SendEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

namespace AlertarApp.Application.DTOs.TrustedContact
{
    public class CreateTrustedContactDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool SendSms { get; set; }
        public bool SendEmail { get; set; }
    }
}

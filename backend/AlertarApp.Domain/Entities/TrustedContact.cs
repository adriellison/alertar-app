using System;

namespace AlertarApp.Domain.Entities
{
    public class TrustedContact
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public bool SendSms { get; private set; }
        public bool SendEmail { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        // Construtor privado para EF Core
        private TrustedContact() { }

        public TrustedContact(Guid userId, string name, string phone, string email = null, bool sendSms = true, bool sendEmail = false)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Phone = phone;
            Email = email;
            SendSms = sendSms;
            SendEmail = sendEmail;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            
            Validate();
        }

        private void Validate()
        {
            if (UserId == Guid.Empty)
                throw new ArgumentException("UserId é obrigatório");
                
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Nome é obrigatório");
                
            if (string.IsNullOrWhiteSpace(Phone))
                throw new ArgumentException("Telefone é obrigatório");
                
            if (SendEmail && string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email é obrigatório quando a opção SendEmail está ativada");
        }

        public void Update(string name, string phone, string email, bool sendSms, bool sendEmail)
        {
            Name = name;
            Phone = phone;
            Email = email;
            SendSms = sendSms;
            SendEmail = sendEmail;
            
            Validate();
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}

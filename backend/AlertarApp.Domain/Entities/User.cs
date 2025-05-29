using System;

namespace AlertarApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string DocumentId { get; private set; }  // CPF
        public string HashedPin { get; private set; }   // PIN criptografado
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public bool IsActive { get; private set; }

        // Construtor privado para EF Core
        private User() { }

        public User(string name, string email, string phone, string documentId, string hashedPin)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            DocumentId = documentId;
            HashedPin = hashedPin;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(Phone))
                throw new ArgumentException("Telefone é obrigatório");
                
            if (string.IsNullOrWhiteSpace(DocumentId))
                throw new ArgumentException("CPF é obrigatório");
                
            if (string.IsNullOrWhiteSpace(HashedPin))
                throw new ArgumentException("PIN é obrigatório");
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.UtcNow;
        }

        public void UpdatePin(string newHashedPin)
        {
            if (string.IsNullOrWhiteSpace(newHashedPin))
                throw new ArgumentException("Novo PIN é obrigatório");
                
            HashedPin = newHashedPin;
        }
    }
}

using System;

namespace AlertarApp.Application.DTOs.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DocumentId { get; set; }  // CPF
        public string Pin { get; set; }
    }
}

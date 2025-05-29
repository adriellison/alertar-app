using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertarApp.Domain.Entities;

namespace AlertarApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByDocumentIdAsync(string documentId);
        Task<User> GetByDocumentIdAndPinAsync(string documentId, string hashedPin);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}

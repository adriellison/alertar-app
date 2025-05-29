using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlertarApp.Domain.Entities;
using AlertarApp.Domain.Interfaces;
using AlertarApp.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AlertarApp.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlertarDbContext _context;

        public UserRepository(AlertarDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByDocumentIdAsync(string documentId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.DocumentId == documentId);
        }

        public async Task<User> GetByDocumentIdAndPinAsync(string documentId, string hashedPin)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.DocumentId == documentId && 
                                         u.HashedPin == hashedPin && 
                                         u.IsActive);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

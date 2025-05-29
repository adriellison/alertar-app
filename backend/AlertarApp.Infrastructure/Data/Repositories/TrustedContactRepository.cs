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
    public class TrustedContactRepository : ITrustedContactRepository
    {
        private readonly AlertarDbContext _context;

        public TrustedContactRepository(AlertarDbContext context)
        {
            _context = context;
        }

        public async Task<TrustedContact> GetByIdAsync(Guid id)
        {
            return await _context.TrustedContacts.FindAsync(id);
        }

        public async Task<IEnumerable<TrustedContact>> GetByUserIdAsync(Guid userId)
        {
            return await _context.TrustedContacts
                .Where(tc => tc.UserId == userId)
                .OrderBy(tc => tc.Name)
                .ToListAsync();
        }

        public async Task AddAsync(TrustedContact trustedContact)
        {
            await _context.TrustedContacts.AddAsync(trustedContact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrustedContact trustedContact)
        {
            _context.TrustedContacts.Update(trustedContact);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var trustedContact = await GetByIdAsync(id);
            if (trustedContact == null)
                return false;

            _context.TrustedContacts.Remove(trustedContact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

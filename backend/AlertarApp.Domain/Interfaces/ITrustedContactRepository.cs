using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertarApp.Domain.Entities;

namespace AlertarApp.Domain.Interfaces
{
    public interface ITrustedContactRepository
    {
        Task<TrustedContact> GetByIdAsync(Guid id);
        Task<IEnumerable<TrustedContact>> GetByUserIdAsync(Guid userId);
        Task AddAsync(TrustedContact trustedContact);
        Task UpdateAsync(TrustedContact trustedContact);
        Task<bool> DeleteAsync(Guid id);
    }
}

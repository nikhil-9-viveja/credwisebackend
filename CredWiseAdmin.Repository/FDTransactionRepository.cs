using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CredWiseAdmin.Repository
{
    public class FDTransactionRepository : GenericRepository<Fdtransaction>, IFDTransactionRepository
    {
        public FDTransactionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Fdtransaction>> GetActiveFDTransactionsByApplicationIdAsync(int fdapplicationId)
        {
            return await _dbSet.Where(x => x.IsActive && x.FdapplicationId == fdapplicationId).ToListAsync();
        }
    }
} 
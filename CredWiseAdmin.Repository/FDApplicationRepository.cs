using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CredWiseAdmin.Repository
{
    public class FDApplicationRepository : GenericRepository<Fdapplication>, IFDApplicationRepository
    {
        public FDApplicationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Fdapplication>> GetActiveFDApplicationsByUserIdAsync(int userId)
        {
            return await _dbSet.Where(x => x.IsActive && x.UserId == userId).ToListAsync();
        }
    }
} 
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CredWiseAdmin.Repository
{
    public class FDTypeRepository : GenericRepository<Fdtype>, IFDTypeRepository
    {
        public FDTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Fdtype>> GetActiveFDTypesByNameAsync(string name)
        {
            return await _dbSet.Where(x => x.IsActive && x.Name.Contains(name)).ToListAsync();
        }

        public override async Task<IEnumerable<Fdtype>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Add FD-specific methods here if needed in the future
    }
}
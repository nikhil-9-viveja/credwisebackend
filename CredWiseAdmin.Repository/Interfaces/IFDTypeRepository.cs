using CredWiseAdmin.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CredWiseAdmin.Repository.Interfaces
{
    public interface IFDTypeRepository : IGenericRepository<Fdtype>
    {
        // Add FD type-specific methods here if needed in the future
    }
} 
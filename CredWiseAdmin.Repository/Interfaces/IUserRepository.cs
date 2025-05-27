using System.Threading.Tasks;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        // You can add more user-related methods here as needed
    }
} 
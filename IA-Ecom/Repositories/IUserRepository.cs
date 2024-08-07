using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUserIdAsync(string userId);
    }
}
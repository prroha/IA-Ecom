using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public interface IUserRepository
    {
        Task<int> CountAllAsync();
        Task<User> GetUserByUserIdAsync(string userId);
        Task<IList<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task SaveChangesAsync();
    }
}
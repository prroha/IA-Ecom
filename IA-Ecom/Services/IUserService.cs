using IA_Ecom.Models;

namespace IA_Ecom.Services
{
    public interface IUserService
    {
        Task<int> CountAllAsync();
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User customer);
        Task UpdateUserAsync(User customer);
        Task DeleteUserAsync(int id);
    }
}

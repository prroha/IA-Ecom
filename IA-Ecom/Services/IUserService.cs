using IA_Ecom.Models;

namespace IA_Ecom.Services
{
    public interface IUserService
    {
        Task<int> CountAllAsync();
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserByUserIdAsync(string id);
        Task UpdateUserAsync(User customer);
        Task UpdateProfileAsync(User user, IFormFile imageInput);
        Task DeleteUserAsync(string id);
    }
}

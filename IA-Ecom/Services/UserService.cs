using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;
using IA_Ecom.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IA_Ecom.Services
{
    public class UserService(IUserRepository customerRepository,
    UserManager<User> userManager
        ) : IUserService
    {
        public async Task<int> CountAllAsync()
        {
            return await customerRepository.CountAllAsync();
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            var users = userManager.Users.ToList(); // Fetch all users
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await customerRepository.GetByIdAsync(id);
        }
        public async Task<User> GetUserByUserIdAsync(string id)
        {
            return await customerRepository.GetUserByUserIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            await customerRepository.AddAsync(user);
            await customerRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            customerRepository.Update(user);
            await customerRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await GetUserByUserIdAsync(userId);
            if (user != null)
            {
                user.DeletedDate = DateTime.UtcNow;
                await customerRepository.SaveChangesAsync();
            }
        }
    }
}
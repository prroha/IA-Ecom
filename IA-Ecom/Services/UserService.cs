using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;

namespace IA_Ecom.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository customerRepository)
        {
            _userRepository = customerRepository;
        }

        public async Task<int> CountAllAsync()
        {
            return await _userRepository.CountAllAsync();
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Remove(user);
                await _userRepository.SaveChangesAsync();
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;
using IA_Ecom.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IA_Ecom.Services
{
    public class UserService(IUserRepository userRepository,
    UserManager<User> userManager,
    IWebHostEnvironment _webHostEnvironment
        ) : IUserService
    {
        public async Task<int> CountAllAsync()
        {
            return await userRepository.CountAllAsync();
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            var users = userManager.Users.ToList(); // Fetch all users
            return users;
        }

        public async Task<User> GetUserByUserIdAsync(string id)
        {
            return await userRepository.GetUserByUserIdAsync(id);
        }


        public async Task UpdateUserAsync(User user)
        {
            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();
        }

        public async Task UpdateProfileAsync(User user, IFormFile imageInput)
        {
            if (imageInput != null && imageInput.Length > 0)
            {
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), user.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                // Save new image
                var filePath = await SaveImageAsync(imageInput);
                user.ImageUrl = filePath;
            }
                await userRepository.UpdateAsync(user);
                // await userRepository.SaveChangesAsync();
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data/Objects");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/App_Data/Objects/{fileName}";
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await GetUserByUserIdAsync(userId);
            if (user != null)
            {
                user.DeletedDate = DateTime.UtcNow;
                await userRepository.SaveChangesAsync();
            }
        }
    }
}
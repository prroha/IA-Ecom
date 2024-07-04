
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using IA_Ecom.Data;
using IA_Ecom.Models;

namespace IA_Test.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                
                var userManager = scopedServices.GetRequiredService<UserManager<User>>();
                var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                // Ensure the database is created.
                context.Database.EnsureCreated();
                
                //create user
                string adminRole = "Admin";
                string adminEmail = "admin@admin.com";
                string adminPassword = "Admin@123";
                string adminFirstName = "Admin";
                string adminLastName = "User";

                // Check if the admin role exists, create it if not
                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                // Check if the admin user exists, create it if not
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true, // Set to true to avoid sending confirmation email
                        FullName = $"{adminFirstName} {adminLastName}",
                        Address = "Admin Address",
                        PhoneNumber = "1234567890",
                        FirstName = adminFirstName,
                        LastName = adminLastName
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                    else
                    {
                        throw new Exception("Failed to create admin user.");
                    }
                }
            }
        }
    }
}

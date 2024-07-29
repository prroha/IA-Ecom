
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

            try
            {
                // Ensure the database is created.
                await context.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                // Log the error 
                // var logger = scopedServices.GetRequiredService<ILogger<SeedData>>();
                // logger.LogError(ex, "An error occurred creating the database.");
                throw new Exception("An error occurred creating the database.", ex);
            }

            await EnsureRoles(roleManager, new[] { "ADMIN", "USER" });
            await EnsureAdmin(userManager, roleManager);
            await EnsureTestUsers(userManager, roleManager);
            // await EnsureTestProducts(context);
        }
    }

    private static async Task EnsureRoles(RoleManager<IdentityRole> roleManager, string[] roles)
    {
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task EnsureAdmin(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminUser = new User
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true,
            Address = "Admin Address",
            PhoneNumber = "1234567890",
            FirstName = "Admin",
            LastName = "User"
        };

        await CreateUserIfNotExists(userManager, adminUser, "Admin@123", "Admin");
    }

    private static async Task EnsureTestUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var testUsers = new List<User>
        {
            new User
            {
                UserName = "user@user.com",
                Email = "user@user.com",
                EmailConfirmed = true,
                Address = "Address 1",
                PhoneNumber = "1234567890",
                FirstName = "User",
                LastName = "One"
            },
            new User
            {
                UserName = "user2@user.com",
                Email = "user2@user.com",
                EmailConfirmed = true,
                Address = "Address 2",
                PhoneNumber = "1234567890",
                FirstName = "User",
                LastName = "Two"
            }
        };

        foreach (var testUser in testUsers)
        {
            await CreateUserIfNotExists(userManager, testUser, "User@123", "User");
        }
    }

    private static async Task CreateUserIfNotExists(UserManager<User> userManager, User user, string password, string role)
    {
        var existingUser = await userManager.FindByEmailAsync(user.Email);
        if (existingUser == null)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception($"Failed to create user {user.Email}");
            }
        }
    }

    private static async Task EnsureTestProducts(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            var testProducts = new List<Product>
            {
                new Product { Name = "Product 1", Description = "Description for product 1", Price = 19.99m, Stock = 100 },
                new Product { Name = "Product 2", Description = "Description for product 2", Price = 29.99m, Stock = 200 },
                new Product { Name = "Product 3", Description = "Description for product 3", Price = 39.99m, Stock = 150 }
            };

            context.Products.AddRange(testProducts);
            await context.SaveChangesAsync();
        }
    }
}
}

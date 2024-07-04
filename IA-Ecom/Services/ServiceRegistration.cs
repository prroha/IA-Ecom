using IA_Ecom.Data;
using IA_Ecom.Models;
using IA_Ecom.Repositories;
using IA_Ecom.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IA_Ecom
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
        }
    }
}
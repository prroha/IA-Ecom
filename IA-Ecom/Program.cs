using IA_Ecom;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IA_Ecom.Data;
using IA_Ecom.Models;
using IA_Test.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder);

var app = builder.Build();
// Seed Data
await SeedData.Initialize(app.Services);

// Configure the HTTP request pipeline
Configure(app, builder.Environment);

app.Run();

// Configure services
static void ConfigureServices(WebApplicationBuilder builder)
{
    IServiceCollection services = builder.Services;
    IConfiguration configuration = builder.Configuration;
    // Determine connection string based on environment
    var connectionString = builder.Environment.IsProduction()
        ? configuration.GetConnectionString("DefaultConnectionSqlServer") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionSqlServer' not found.")
        : configuration.GetConnectionString("DefaultConnectionSqlite") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionSqlite' not found.");
    // var connectionString = configuration.GetConnectionString("DefaultConnectionSqlite") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionSqlite' not found.");

    // Register ApplicationDbContext with the chosen database provider
        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseSqlite(connectionString));
    if (builder.Environment.IsProduction())
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    }
    else
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
    }
    
    services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    services.AddControllersWithViews();
    // Register application services
    services.AddAutoMapper(typeof(Program));
    ServiceRegistration.RegisterServices(services, configuration);
    
    // Configure Identity options
    services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
}

// Configure the HTTP request pipeline
static void Configure(WebApplication app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();
}
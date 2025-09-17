using EmployeeTraingsTracker.Components;
using EmployeeTraingsTracker.Components.Account;
using EmployeeTraingsTracker.Data;
using EmployeeTraingsTracker.Model;
using EmployeeTraingsTracker.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


using System;
namespace EmployeeTraingsTracker
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Database & Identity setup
            // -----------------------------
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Full Identity setup
                    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                    })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<IdentityUserAccessor>();


            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ITrainingService, TrainingService>();
            builder.Services.AddScoped<IEmployeeTrainingService, EmployeeTrainingService>();
            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();


            var app = builder.Build();

            // -----------------------------
            // Seed Roles and Admin User
            // -----------------------------
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string[] roles = { "Admin", "Employee" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Seed default admin
                string adminEmail = "admin@local.com";
                string adminPass = "Admin@123";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    var createResult = await userManager.CreateAsync(adminUser, adminPass);
                    if (createResult.Succeeded)
                    {
                        var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                        Console.WriteLine($"Admin user created: {createResult.Succeeded}");
                        Console.WriteLine($"Admin role assigned: {roleResult.Succeeded}");
                    }
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();
            app.MapRazorPages();


            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

          app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
    //public class Program
    //{
    //    public static async Task Main(string[] args)
    //    {
    //        var builder = WebApplication.CreateBuilder(args);

    //        // -----------------------------
    //        // Database & Identity setup
    //        // -----------------------------
    //        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    //                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    //        builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //            options.UseSqlServer(connectionString));

    //        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    //        // Full Identity setup
    //        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    //        {
    //            options.SignIn.RequireConfirmedAccount = false;
    //        })
    //        .AddEntityFrameworkStores<ApplicationDbContext>()
    //        .AddDefaultTokenProviders();


    //        builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
    //        builder.Services.AddRazorPages();

    //        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

    //        // -----------------------------
    //        // Add custom services
    //        // -----------------------------
    //        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    //        builder.Services.AddScoped<ITrainingService, TrainingService>();
    //        builder.Services.AddScoped<IEmployeeTrainingService, EmployeeTrainingService>();
    //        // Add this after your Identity setup
    //        builder.Services.AddScoped<IdentityRedirectManager>();
    //        // -----------------------------
    //        // Add Blazor services
    //        // -----------------------------
    //        builder.Services.AddRazorComponents()
    //               .AddInteractiveServerComponents();
    //        builder.Services.AddCascadingAuthenticationState();

    //        // -----------------------------
    //        // Build app
    //        // -----------------------------
    //        var app = builder.Build();

    //        // -----------------------------
    //        // Seed Roles and Admin User
    //        // -----------------------------
    //        using (var scope = app.Services.CreateScope())
    //        {
    //            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    //            string[] roles = { "Admin", "Employee" };

    //            foreach (var role in roles)
    //            {
    //                if (!await roleManager.RoleExistsAsync(role))
    //                    await roleManager.CreateAsync(new IdentityRole(role));
    //            }

    //            // Seed default admin
    //            string adminEmail = "admin@local.com";
    //            string adminPass = "Admin@123";

    //            var adminUser = await userManager.FindByEmailAsync(adminEmail);
    //            if (adminUser == null)
    //            {
    //                adminUser = new ApplicationUser
    //                {
    //                    UserName = adminEmail,
    //                    Email = adminEmail,
    //                    EmailConfirmed = true
    //                };
    //                var createResult = await userManager.CreateAsync(adminUser, adminPass);
    //                if (createResult.Succeeded)
    //                {
    //                    var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
    //                    Console.WriteLine($"Admin user created: {createResult.Succeeded}");
    //                    Console.WriteLine($"Admin role assigned: {roleResult.Succeeded}");
    //                }
    //            }
    //        }

    //        // -----------------------------
    //        // HTTP Pipeline
    //        // -----------------------------
    //        if (!app.Environment.IsDevelopment())
    //        {
    //            app.UseExceptionHandler("/Error");
    //            app.UseHsts();
    //        }
    //        else
    //        {
    //            app.UseMigrationsEndPoint();
    //        }

    //        app.UseHttpsRedirection();
    //        app.UseStaticFiles();
    //        app.UseRouting();

    //        app.UseAuthentication();
    //        app.UseAuthorization();
    //        app.UseAntiforgery();
    //        app.MapRazorPages();

    //        app.MapBlazorHub();

    //        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();




    //        app.MapAdditionalIdentityEndpoints();

    //        app.Run();
    //    }
    //}
}


using AG.Core.Policy;
using AG.Data;
using AG.Services.Repository;
using AG.Services.Timesheet;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                opts =>
            {
                opts.AccessDeniedPath = "/Account/Login";
                opts.LoginPath = "/Account/Login";
                opts.LogoutPath = "/Account/Logout";
            });
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminMod", p => p.RequireRole(DefaultRoles.ADMIN, DefaultRoles.MODERATOR));
                opt.AddPolicy("AdminModHR", p => p.RequireRole(DefaultRoles.ADMIN, DefaultRoles.MODERATOR, DefaultRoles.HR));
            });

            var connString = builder.Configuration.GetConnectionString("SqliteConnection");
            builder.Services
                .AddDbContext<DataContext>(opts => opts.UseSqlite(connString), ServiceLifetime.Scoped)
                .AddTransient<EmployeeFunctionService>()
                .AddTransient<TimesheetService>()
                .AddTransient<TimeIntervalService>()
                .AddTransient<AuthenticationService>()
                .AddTransient<UserService>()
                .AddTransient<EmployeeTimeIntervalService>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapAreaControllerRoute(
               name: "profile",
               areaName: "Account",
               pattern: "{controller=Account}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
               name: "admin",
               areaName: "Admin",
               pattern: "admin/{controller=Users}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
               name: "hr",
               areaName: "HR",
               pattern: "hr/{controller=Main}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
           
            app.Run();
        }
    }
}

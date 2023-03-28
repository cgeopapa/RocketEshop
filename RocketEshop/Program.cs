using System.Globalization;
using RocketEshop.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Enums;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure;

namespace RocketEshop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SetUpDbAndRepositories(builder);
            var localizationOptions = SetUpLocalization(builder);
            SetUpSession(builder);
            SetupIdentity(builder);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            SetUpApp(app, localizationOptions);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            if (builder.Configuration.GetValue<bool>("InitializeDB"))
            {
                await AppDbInitializer.Seed(app);
            }

            app.Run();
        }

        private static void SetUpApp(WebApplication app, RequestLocalizationOptions localizationOptions)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseRequestLocalization(localizationOptions);
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void SetupIdentity(WebApplicationBuilder builder)
        {
            // Identity Services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(UserRole.ADMIN.ToString()));
            });
        }

        private static void SetUpSession(WebApplicationBuilder builder)
        {
            // Session
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSession();
        }

        private static RequestLocalizationOptions SetUpLocalization(WebApplicationBuilder builder)
        {
            // Set up available localizations
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("el-GR")
            };
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };
            builder.Services.AddSingleton<RequestLocalizationOptions>(localizationOptions);

            // Where to find translation resources
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddRazorPages().AddViewLocalization();
            return localizationOptions;
        }

        private static void SetUpDbAndRepositories(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            builder.Services.AddTransient<IGamesService, GamesService>();
            builder.Services.AddTransient<IGenresService, GenresService>();
            builder.Services.AddTransient<IOrdersService, OrdersService>();
            builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
        }
    }
}
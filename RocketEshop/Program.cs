using System.Globalization;
using RocketEshop.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure;
using RocketEshop.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace RocketEshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            
            builder.Services.AddTransient<IGamesService, GamesService>();
            builder.Services.AddTransient<IGenresService, GenresService>();
            builder.Services.AddTransient<IOrdersService, OrdersService>();

            // Set up available localizations
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("el")
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

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddViewLocalization();

            // Session
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
            builder.Services.AddSession();

            // Identity Services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            // Session
            app.UseSession();

            app.UseRouting();

            // Use Localization
            app.UseRequestLocalization(localizationOptions);

            //Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            if (builder.Configuration.GetValue<bool>("InitializeDB"))
            {
                AppDbInitializer.Seed(app);
                AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
            }

            app.Run();
        }
    }
}
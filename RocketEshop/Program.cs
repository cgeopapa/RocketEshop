using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure;
using RocketEshop.Infrastructure.Services;

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

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Session
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
            builder.Services.AddSession();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            if (builder.Configuration.GetValue<bool>("InitializeDB"))
            {
                AppDbInitializer.Seed(app);
            }

            app.Run();
        }
    }
}
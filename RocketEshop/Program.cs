using Microsoft.EntityFrameworkCore;
using RocketEshop.Data;
using RocketEshop.Data.Services;
using System.Configuration;

namespace RocketEshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IGamesService, GamesService>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Games}/{action=Index}/{id?}");

            AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}
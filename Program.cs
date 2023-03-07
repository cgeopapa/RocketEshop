using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RocketEshop.Data;
using RocketEshop.Data.Services;

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
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=ALEXMET2;Initial Catalog=RocketEshop;Integrated Security=True"));

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            MockDbInitializer.AddMockData(app);

            app.Run();
        }
    }
}
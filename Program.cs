using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RocketEshop.Data;

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
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Filename=app.db"));

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

            // Gets value from appsettings.json at "AddMockDbData"
            if (builder.Configuration.GetValue<bool>("AddMockDbData"))
            { 
                MockDbInitializer.AddMockData(app);
            }

            app.Run();
        }
    }
}
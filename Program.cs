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

            //var cnn = new SqliteConnection("Data Source=app.db");
            //cnn.Open();
            //builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(cnn));

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
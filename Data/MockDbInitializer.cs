using RocketEshop.Models;

namespace RocketEshop.Data
{
    public class MockDbInitializer
    {
        public static void AddMockData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            AppDbContext? db = scope.ServiceProvider.GetService<AppDbContext>();
            if(db == null)
            {
                Console.Error.WriteLine("could not get AppDbContext service");
                return;
            }

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            db.games.Add(new Game {
                Title = "Skata1",
                Description = "Kati1",
                Price = 10,
                ImageUrl = "skata123",
                Features = "skate123",
                Quantity = 10,
                Availability = true,
                Rating = Enums.Rating.Average
            });
            db.SaveChanges();
        }
    }
}

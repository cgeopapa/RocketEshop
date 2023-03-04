using RocketEshop.Models;

namespace RocketEshop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<AppDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var game1 = new Game
            {
                GameId = 1,
                Title = "Skata1",
                Description = "Kati1",
                Price = 10,
                ImageUrl = "skata123",
                Features = "skate123",
                Quantity = 10,
                Availability = true,
                Rating = Enums.Rating.Average
            };
            

            db.games.Add(game1);
            

            db.SaveChanges();
        }
    }
}

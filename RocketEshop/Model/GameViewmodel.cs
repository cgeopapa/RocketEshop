using RocketEshop.Core.Enums;
using RocketEshop.Core.Models;

namespace RocketEshop.Model
{
    public class GameViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public Rating Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public List<int> Genres { get; set; }

        public GameViewModel() { }

        public GameViewModel(Game game)
        {
            Id = game.Id;
            Title = game.Title;
            Price = game.Price;
            ImageUrl = game.ImageUrl;
            Quantity = game.Quantity;
            Rating = game.Rating;
            ReleaseDate = game.Release_Date;
            Description = game.Description;

            Genres = new List<int>();
            foreach (Genre genre in game.Genres)
            {
                Genres.Add(genre.Id);
            }
        }

    }
}

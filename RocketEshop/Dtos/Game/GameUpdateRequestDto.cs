using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Models;
using RocketEshop.Core.Enums;

namespace RocketEshop.Dtos.Game
{
    public class GameUpdateRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public Rating Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public List<int> Genres { get; set; }

        public SelectList genreOptions { get; set; }

        public GameUpdateRequestDto() { }

        public GameUpdateRequestDto(Core.Models.Game game, SelectList genreOptions)
        {
            this.gameFromGameEntity(game);
            this.genreOptions = genreOptions;
        }

        private void gameFromGameEntity(Core.Models.Game game)
        {
            this.Id = game.Id;
            this.Title = game.Title;
            this.Price = game.Price;
            this.ImageUrl = game.ImageUrl;
            this.Quantity = game.Quantity;
            this.Rating = game.Rating;
            this.ReleaseDate = game.Release_Date;
            this.Description = game.Description;

            this.Genres = new List<int>();
            foreach (Genre genre in game.Genres)
            {
                this.Genres.Add(genre.Id);
            }
        }
    }
}

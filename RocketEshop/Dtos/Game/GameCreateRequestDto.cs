using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Models;
using RocketEshop.Core.Enums;

namespace RocketEshop.Dtos.Game
{
    public class GameCreateRequestDto
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public Rating Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public List<int> Genres { get; set; }

        public SelectList genreOptions { get; set; }

        public GameCreateRequestDto() { }

        public GameCreateRequestDto(SelectList genreOptions)
        {
            this.genreOptions = genreOptions;
        }
    }
}

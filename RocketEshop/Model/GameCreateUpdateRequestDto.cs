using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Models;

namespace RocketEshop.Model
{
    public class GameCreateUpdateRequestDto
    {
        public Game? game;
        public List<SelectListItem> genres = new List<SelectListItem>();

        public GameCreateUpdateRequestDto() { }

        public GameCreateUpdateRequestDto(Game game, List<SelectListItem> genres)
        {
            this.game = game;
            this.genres = genres;
        }

        public GameCreateUpdateRequestDto(Game game)
        {
            this.game = game;
        }

        public GameCreateUpdateRequestDto(List<SelectListItem> genres)
        {
            this.genres = genres;
        }
    }
}

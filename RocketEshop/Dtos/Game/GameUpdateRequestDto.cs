using Microsoft.AspNetCore.Mvc.Rendering;

namespace RocketEshop.Dtos.Game
{
    public class GameUpdateRequestDto
    {
        public GameDto Game { get; set; }
        public SelectList genreOptions { get; set; }

        public GameUpdateRequestDto() { }

        public GameUpdateRequestDto(Core.Models.Game game, SelectList genreOptions)
        {
            this.Game = new GameDto(game);
            this.genreOptions = genreOptions;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace RocketEshop.Dtos.Game
{
    public class GameCreateRequestDto
    {
        public GameDto Game { get; set; }

        public SelectList genreOptions { get; set; }

        public GameCreateRequestDto() { }

        public GameCreateRequestDto(SelectList genreOptions)
        {
            this.Game = new GameDto();
            this.genreOptions = genreOptions;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Models;

namespace RocketEshop.Model.Games
{
    public class Edit
    {
        public GameViewModel GameViewModel { get; set; }
        public SelectList genreOptions { get; set; }

        public Edit() { }

        public Edit(Game game, SelectList genreOptions)
        {
            this.GameViewModel = new GameViewModel(game);
            this.genreOptions = genreOptions;
        }
    }
}

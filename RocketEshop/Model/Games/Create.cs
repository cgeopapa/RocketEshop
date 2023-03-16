using Microsoft.AspNetCore.Mvc.Rendering;

namespace RocketEshop.Model.Games
{
    public class Create
    {
        public GameViewModel GameViewModel { get; set; }
        public SelectList genreOptions { get; set; }

        public Create() { }

        public Create(SelectList genreOptions)
        {
            this.GameViewModel = new GameViewModel();
            this.genreOptions = genreOptions;
        }
    }
}

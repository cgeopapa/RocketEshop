using RocketEshop.Core.Models;

namespace RocketEshop.Infrastructure.Data.ViewModel
{
    public class HomeVM
    {
        public List<GameVM> Games { get; set; }
        public string? QuickSearchFilter { get; set; }

        public HomeVM() { }

        public HomeVM(IEnumerable<Game> games, string? quickSearchFilter)
        {
            this.Games = new List<GameVM>();
            foreach (Game game in games)
            {
                this.Games.Add(new GameVM(game));
            }

            this.QuickSearchFilter = quickSearchFilter;
        }
    }
}

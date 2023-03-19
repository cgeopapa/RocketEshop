using RocketEshop.Core.Domain;
using RocketEshop.Core.Models;

namespace RocketEshop.Infrastructure.Data.ViewModel
{
    public class BrowseVM
    {
        public IEnumerable<GameVM> Games { get; set; }
        public Filters Filters { get; set; }
        public BrowseVM() { }

        public BrowseVM(IEnumerable<Game> games, Filters filters)
        {
            this.Games = new List<GameVM>();
            foreach (Game game in games)
            {
                this.Games = this.Games.Append(new GameVM(game));
            }

            this.Filters = filters;
        }
    }
}

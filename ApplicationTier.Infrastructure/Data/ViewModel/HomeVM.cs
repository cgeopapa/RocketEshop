using RocketEsgop.Infrastructure.Data.ViewModel;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Models;

namespace RocketEshop.Infrastructure.Data.ViewModel
{
    public class HomeVM
    {
        public List<GameVM> Games { get; set; }
        public Filters Filters { get; set; }

        public HomeVM() { }

        public HomeVM(IEnumerable<Game> games, Filters filters)
        {
            this.Games = new List<GameVM>();
            foreach (Game game in games)
            {
                this.Games.Add(new GameVM(game));
            }
            Filters = filters;
        }
    }
}

using RocketEshop.Core.Models;

namespace RocketEshop.Infrastructure.Data.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<GameVM> LatestReleases { get; set; }
        public IEnumerable<GameVM> GoodRatings { get; set; }
        public string? QuickFilter { get; set; } = null;

        public HomeVM() { }

        public HomeVM(IEnumerable<Game> latestReleases, IEnumerable<Game> goodRatings, string quickFilter)
        {
            this.LatestReleases = new List<GameVM>();
            foreach (Game game in latestReleases)
            {
                this.LatestReleases = this.LatestReleases.Append(new GameVM(game));
            }

            this.GoodRatings = new List<GameVM>();
            foreach (Game game in goodRatings)
            {
                this.GoodRatings = this.GoodRatings.Append(new GameVM(game));
            }
            
            this.QuickFilter = quickFilter;
        }
    }
}

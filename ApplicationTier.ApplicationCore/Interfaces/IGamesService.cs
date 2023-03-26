using RocketEshop.Core.Domain;
using RocketEshop.Core.Models;

namespace RocketEshop.Core.Interfaces
{
    public interface IGamesService : IRepository<Game, int>
    {
        IEnumerable<Game> FetchAllWithGenres();
        IEnumerable<Game> FetchFilteredGamesList(Filters filters);
        IEnumerable<Game> FetchLatestReleasedGames(int? maxResults);
        IEnumerable<Game> FetchGoodRatedGames(int? maxResults);
        void BulkUploadGames(List<Game> games);
    }
}

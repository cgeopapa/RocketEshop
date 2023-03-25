using RocketEshop.Core.Models;
using RocketEshop.Core.Domain;

namespace RocketEshop.Core.Interfaces
{
    public interface IGamesService : IRepository<Game>
    {
        IEnumerable<Game> FetchAllWithGenres();
        IEnumerable<Game> FetchFilteredGamesList(Filters filters);
        IEnumerable<Game> FetchLatestReleasedGames(int? maxResults);
        IEnumerable<Game> FetchGoodRatedGames(int? maxResults);
        void BulkUploadGames(List<Game> games);
    }
}

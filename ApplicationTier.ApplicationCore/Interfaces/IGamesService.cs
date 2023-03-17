using RocketEshop.Core.Models;
using RocketEshop.Core.Domain;

namespace RocketEshop.Core.Interfaces
{
    public interface IGamesService : IRepository<Game>
    {
        IEnumerable<Game> FetchAllWithGenres();
        IEnumerable<Game> FetchFilteredGamesList(Filters filters);
    }
}

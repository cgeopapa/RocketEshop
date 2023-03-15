using RocketEshop.Core.Models;

namespace RocketEshop.Core.Interfaces
{
    public interface IGamesService : IRepository<Game>
    {
        IEnumerable<Game> GetByQuickSearchFilter(string quickSearchFilter);
    }
}

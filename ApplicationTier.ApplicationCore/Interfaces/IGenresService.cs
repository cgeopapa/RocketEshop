using RocketEshop.Core.Models;

namespace RocketEshop.Core.Interfaces
{
    public interface IGenresService : IRepository<Genre, int>
    {
        Genre? FetchGenreByName(string name);
    }
}

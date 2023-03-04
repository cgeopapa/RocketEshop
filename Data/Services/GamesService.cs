using RocketEshop.Data.Base;
using RocketEshop.Models;

namespace RocketEshop.Data.Services
{
    public class GamesService : EntityBaseRepository<Game>, IGamesService
    {
        public GamesService(AppDbContext context) : base(context)
        {
        }
    }
}

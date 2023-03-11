using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Repositories;

namespace RocketEshop.Infrastructure.Services
{
    public class GamesService : EntityBaseRepository<Game>, IGamesService
    {
        public GamesService(AppDbContext context) : base(context)
        {
        }
    }
}

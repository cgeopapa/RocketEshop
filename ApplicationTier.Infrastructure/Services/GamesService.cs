using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Repositories;

namespace RocketEshop.Infrastructure.Services
{
    public class GamesService : EntityBaseRepository<Game>, IGamesService
    {
        private readonly AppDbContext context;
        public GamesService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        
        public new async Task<Game?> GetByIdAsync(int id)
        {
            Game? entity = await context.Games.Include(x => x.Genres).FirstOrDefaultAsync(x => x.Id == id);
            return entity ?? null;
        }

        public new async Task UpdateAsync(Game game)
        {
            var existingGame = await GetByIdAsync(game.Id);
            if (existingGame != null)
            {
                context.Remove(existingGame);
            }
            context.Update(game);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Game> GetByQuickSearchFilter(string quickSearchFilter)
        {
            var games = context.Games.Include(x => x.Genres).Where(game => game.Title.Contains(quickSearchFilter));
            return games;
        }
    }
}

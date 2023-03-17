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
            Game? entity = await context.Games.Include(x => x.GameGenreLink).ThenInclude(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);
            return entity ?? null;
        }

        public new async Task UpdateAsync(Game game)
        {
            var existingGame = await GetByIdAsync(game.Id);
            if (existingGame == null)
            {
                throw new Exception($"Cannot find game with id {game.Id}");
            }
            existingGame.Title = game.Title;
            existingGame.Description = game.Description;
            existingGame.Release_Date = game.Release_Date;
            existingGame.ImageUrl = game.ImageUrl;
            existingGame.Price = game.Price;
            existingGame.Quantity = game.Quantity;
            existingGame.Rating = game.Rating;

            List<GameGenre> gameGenresForRemove = new List<GameGenre>();
            foreach (GameGenre gameGenre in existingGame.GameGenreLink)
            {
                if (!game.GameGenreLink.Contains(gameGenre))
                {
                    gameGenresForRemove.Add(gameGenre);
                }
            }
            var gameGenresRemoved = existingGame.GameGenreLink.Except(gameGenresForRemove).ToList();
            existingGame.GameGenreLink = gameGenresRemoved;

            List<GameGenre> gameGenresForAdd = new List<GameGenre>();
            foreach (GameGenre gameGenre in game.GameGenreLink)
            {
                if (!existingGame.GameGenreLink.Contains(gameGenre))
                {
                    gameGenresForAdd.Add(gameGenre);
                }
            }
            existingGame.GameGenreLink.AddRange(gameGenresForAdd);

            await context.SaveChangesAsync();
        }

        public IEnumerable<Game> GetByQuickSearchFilter(string quickSearchFilter)
        {
            var games = context.Games.Include(x => x.GameGenreLink).Where(game => game.Title.Contains(quickSearchFilter));
            return games;
        }
    }
}

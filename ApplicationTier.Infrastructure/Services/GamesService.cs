using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Enums;
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
            Game? entity = await context.Games
                .Include(x => x.GameGenreLink)
                .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);
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
            existingGame.ReleaseDate = game.ReleaseDate;
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

        public IEnumerable<Game> FetchAllWithGenres()
        {
            return context.Games
                .Include(x => x.GameGenreLink)
                .ThenInclude(x => x.Genre);
        }

        public IEnumerable<Game> FetchFilteredGamesList(Filters filters)
        {
            var games = context.Games.Include(x => x.GameGenreLink).ThenInclude(x => x.Genre);
            IEnumerable<Game> filteredGames = new List<Game>(games);

            if (filters.QuickSearchFilter != null)
            {
                filteredGames = filteredGames.Where(game => game.Title.ToLower().Contains(filters.QuickSearchFilter.ToLower()));
            }
            if (filters.Availability)
            {
                filteredGames = filteredGames.Where(game => game.Quantity > 0);
            }
            filteredGames = filteredGames.Where(game => game.Price >= filters.MinPrice && game.Price <= filters.MaxPrice);

            switch (filters.Sorting)
            {
                case SortingFilter.NameAsc:
                    filteredGames = filteredGames.OrderBy(game => game.Title);
                    break;
                case SortingFilter.NameDesc:
                    filteredGames = filteredGames.OrderByDescending(game => game.Title);
                    break;
                case SortingFilter.PriceAsc:
                    filteredGames = filteredGames.OrderBy(game => game.Price);
                    break;
                case SortingFilter.PriceDsc:
                    filteredGames = filteredGames.OrderByDescending(game => game.Price);
                    break;
                case SortingFilter.RatingAsc:
                    filteredGames = filteredGames.OrderByDescending(game => game.Rating);
                    break;
                case SortingFilter.RatingDsc:
                    filteredGames = filteredGames.OrderBy(game => game.Rating);
                    break;
                case SortingFilter.ReleaseDateAsc:
                    filteredGames = filteredGames.OrderBy(game => game.ReleaseDate);
                    break;
                case SortingFilter.ReleaseDateDsc:
                    filteredGames = filteredGames.OrderByDescending(game => game.ReleaseDate);
                    break;
            }

            return filteredGames;
        }

        public IEnumerable<Game> FetchLatestReleasedGames(int? maxResults)
        {
            var games = context.Games.Include(x => x.GameGenreLink).ThenInclude(x => x.Genre);
            return games.OrderBy(game => game.ReleaseDate).Take(maxResults ?? 3);
        }

        public IEnumerable<Game> FetchGoodRatedGames(int? maxResults)
        {
            var games = context.Games.Include(x => x.GameGenreLink).ThenInclude(x => x.Genre);
            return games.OrderByDescending(game => game.Rating).Take(maxResults ?? 3);
        }

        public void BulkUploadGames(List<Game> games)
        {
            context.Games.AddRange(games);
            context.SaveChanges();
        }
    }
}

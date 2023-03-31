using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Repositories;

namespace RocketEshop.Infrastructure.Services
{
    public class GenresService : EntityBaseRepository<Genre, int>, IGenresService
    {
        private readonly AppDbContext context;
        
        public GenresService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public Genre? FetchGenreByName(string name)
        {
            return context.Genres.FirstOrDefault(genre => genre.Name == name);
        }
    }
}

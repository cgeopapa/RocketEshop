using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Repositories;

namespace RocketEshop.Infrastructure.Services
{
    public class GenresService : EntityBaseRepository<Genre>, IGenresService
    {
        public GenresService(AppDbContext context) : base(context)
        {
        }
    }
}

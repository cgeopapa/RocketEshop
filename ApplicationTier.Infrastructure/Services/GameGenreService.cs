﻿using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Repositories;

namespace RocketEshop.Infrastructure.Services
{
    public class GameGenreService : EntityBaseRepository<GameGenre, int>, IGameGenreService
    {
        public GameGenreService(AppDbContext context) : base(context)
        {
        }
    }
}

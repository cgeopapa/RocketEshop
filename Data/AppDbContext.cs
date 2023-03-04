using Microsoft.EntityFrameworkCore;
using RocketEshop.Models;

namespace RocketEshop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        public DbSet<Game> games { get; set; }
    }
}

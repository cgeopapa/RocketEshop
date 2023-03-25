using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Infrastructure.Repositories
{
    public class EntityBaseRepository<T> : IRepository<T> where T : class, IEntity, new()
    {

        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            T? entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

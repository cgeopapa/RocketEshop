using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Infrastructure.Repositories
{
    public class EntityBaseRepository<T, TK> : IRepository<T, TK> where T : class, IEntity<TK>, new() where TK: IConvertible
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

        public async Task<T?> GetByIdAsync(TK id)
        {
            T? entity = await _context.Set<T>().FirstOrDefaultAsync(x => Equals(x.Id, id));
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

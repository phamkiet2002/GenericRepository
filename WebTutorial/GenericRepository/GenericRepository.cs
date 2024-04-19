
using Microsoft.EntityFrameworkCore;
using WebAPI_Tutorial.Data;

namespace WebTutorial.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _dbContext;
        protected DbSet<T> _dbSet;
        public GenericRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                return null;
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var delete = await _dbSet.FindAsync(id);
            if (delete == null)
                return null;
            _dbSet.Remove(delete);
            await _dbContext.SaveChangesAsync();
            return delete;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getId = await _dbSet.FindAsync(id);
            if (getId == null)
                return null;
            return getId;
        }

        public async Task<List<T>> GettAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                return null;
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

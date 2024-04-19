using Microsoft.EntityFrameworkCore;
using WebAPI_Tutorial.Data;
using WebAPI_Tutorial.Model;

namespace WebTutorial.GenericRepository
{
    public interface IStockGenericRepository : IGenericRepository<StockEntity>
    {
        //Task<List<StockEntity>> GettAllAsync();
        //Task<int> GetTotalStock(int id);
        //Task<(List<StockEntity>, int)> GettAllAsync(int currentPage, int pageSize, string sortBy);
        Task<List<StockEntity>> GettAllSortByAsync(string sortBy);
    }
    public class StockGenericRepository : GenericRepository<StockEntity>, IStockGenericRepository
    {
        public StockGenericRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<StockEntity>> GettAllAsync()
        {
            return await _dbSet.Include(s => s.Comments).ToListAsync();
        }
        public async Task<StockEntity> GetByIdAsync(int id)
        {
            return await _dbSet.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<StockEntity>> GettAllSortByAsync(string sortBy)
        {
            IQueryable<StockEntity> query = _dbSet.Include(s => s.Comments);

            if (!string.IsNullOrWhiteSpace(sortBy) && sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderBy(s => s.Id);
            }

            return await query.ToListAsync();
        }
    }
}
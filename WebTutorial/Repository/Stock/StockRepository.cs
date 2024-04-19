using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI_Tutorial.Data;
using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.Stock;
using WebTutorial.Helper;


namespace WebTutorial.Repository.Stock
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public StockRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StockEntity> Create(StockEntity entity)
        {
            await _dbContext.Stocks.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<StockEntity>> GetAllQuery(QueryObject query)
        {
            var stock = _dbContext.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Company))
            {
                stock = stock.Where(c => c.Company.Contains(query.Company));
            }
            if (!string.IsNullOrWhiteSpace(query.Sybol))
            {
                stock = stock.Where(c => c.Sybol.Contains(query.Sybol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = stock.OrderBy(s => s.Sybol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stock.Skip((int)skipNumber).Take((int)query.PageSize).ToListAsync();
        }


        public async Task<StockEntity?> Delete(int id)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _dbContext.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<List<StockEntity>> GettAll()
        {
            return await _dbContext.Stocks.Include(s => s.Comments).ToListAsync();
        }

        public async Task<StockEntity?> GettById(int id)
        {
            return await _dbContext.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
           return _dbContext.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<StockEntity?> Update(int id, UpdateStockRequestDto StockDto)
        {
            var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
                return null;
            stockModel.Sybol = StockDto.Sybol;
            stockModel.Company = StockDto.Company;
            stockModel.Purchase = StockDto.Purchase;
            stockModel.LastDiv = StockDto.LastDiv;
            stockModel.Industry = StockDto.Industry;
            stockModel.MarketCap = StockDto.MarketCap;
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }
    }
}

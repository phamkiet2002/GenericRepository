using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.Stock;
using WebTutorial.Helper;

namespace WebTutorial.Repository.Stock
{
    public interface IStockRepository
    {
        Task<List<StockEntity>> GettAll();
        Task<StockEntity?> GettById(int id);
        Task<StockEntity> Create(StockEntity entity);
        Task<StockEntity?> Update(int id, UpdateStockRequestDto StockDto);
        Task<StockEntity?> Delete(int id);
        Task<bool> StockExists(int id);
        Task<List<StockEntity>> GetAllQuery(QueryObject query);
    }
}

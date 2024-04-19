using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Tutorial.Model;
using WebTutorial.Mapper;
using WebTutorial.Repository.Stock;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebTutorial.GenericRepository
{
    [Route("api/testGeneric")]
    [ApiController]
    public class TestGenericRepositoryController : ControllerBase
    {
        private readonly IStockGenericRepository _stockGenericRepository;
        public TestGenericRepositoryController(IStockGenericRepository stockGenericRepository)
        {
            _stockGenericRepository = stockGenericRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var stock = await _stockGenericRepository.GettAllAsync();
            var stockdto = stock.Select(s => s.toStockDto());
            return Ok(stockdto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockGenericRepository.GetByIdAsync(id);
            if (stock == null) 
                return NotFound();
            return Ok(stock.toStockDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var stock = await _stockGenericRepository.DeleteAsync(id);
            if(stock == null)
                return NotFound();
            return Ok("xoa thanh cong" + stock);
        }

        [HttpGet("sortBy")]
        public async Task<IActionResult> GetAllStocks([FromQuery] string sortBy)
        {
            try
            {
                var stocks = await _stockGenericRepository.GettAllSortByAsync(sortBy);
                var stockDtos = stocks.Select(s => s.toStockDto());
                return Ok(stockDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

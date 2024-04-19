using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Tutorial.Data;
using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.Stock;
using WebTutorial.Helper;
using WebTutorial.Mapper;
using WebTutorial.Repository.Comment;
using WebTutorial.Repository.Stock;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebTutorial.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository StockRepository;
        public StockController(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await StockRepository.GettAll();
            var stockdto = stock.Select(s => s.toStockDto());
           // var stockdto = _mapper.Map<List<StockDto>>(stock);
            return Ok(stockdto);
        }
        [HttpGet("query")]
        public async Task<IActionResult> GetAllQuery([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await StockRepository.GetAllQuery(query);
            var commentDto = stock.Select(c => c.toStockDto());
            return Ok(commentDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await StockRepository.GettById(id);
            if (stock == null)
            {
                return NotFound();
            }
            //var stockdto = _mapper.Map<StockDto>(stock);
            return Ok(stock.toStockDto());
            //return Ok(stockdto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto StockDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = StockDTO.ToStockcreateDto();
            await StockRepository.Create(stockModel);
            return Ok("tao thanh cong " + stockModel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await StockRepository.Update(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            //AutoMapper.map(updateDto, stockModel);
            return Ok("cap nha thanh cong" + stockModel);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await StockRepository.Delete(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Content("Xóa Thành công");
        }
    }
}
using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.Stock;

namespace WebTutorial.Mapper
{
    public static class StockMapper
    {
        public static StockDto toStockDto(this StockEntity stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Sybol = stockModel.Sybol,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.toCommentDto()).ToList(),
            };
        }

        public static StockEntity ToStockcreateDto(this CreateStockRequestDto stockDto)
        {
            return new StockEntity
            {
                Sybol = stockDto.Sybol,
                Company = stockDto.Company,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }
    }
}

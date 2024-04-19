using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebTutorial.Dtos.CommentDto;
using WebAPI_Tutorial.Model;

namespace WebTutorial.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Sybol { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public ICollection<CommentDtos> Comments { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Tutorial.Model
{
    public class StockEntity
    {
        [Key]
        public int Id { get; set; }
        public string Sybol { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Tutorial.Model
{
    public class CommentEntity
    {
        
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Nhập lớn hơn 5 ký tự")]
        [MaxLength(255, ErrorMessage = "Không thể vượt quá 255 ký tự")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Nhập lớn hơn 5 ký tự")]
        [MaxLength(255, ErrorMessage = "Không thể vượt quá 255 ký tự")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int StockId { get; set; }
        public StockEntity Stock { get; set; }
    }
}

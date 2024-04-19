using System.ComponentModel.DataAnnotations;

namespace WebTutorial.Dtos.CommentDto
{
    public class CreateCommentDtos
    {
        [Required]
        [MinLength(5, ErrorMessage = "Nhập lớn hơn 5 ký tự")]
        [MaxLength(255, ErrorMessage = "Không thể vượt quá 255 ký tự")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Nhập lớn hơn 5 ký tự")]
        [MaxLength(255, ErrorMessage = "Không thể vượt quá 255 ký tự")]
        public string Content { get; set; } = string.Empty;
        public int? StockId { get; set; }
    }
}

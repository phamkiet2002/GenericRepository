using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.CommentDto;
using WebTutorial.Dtos.Stock;

namespace WebTutorial.Mapper
{
    public static class CommentMapper
    {
        public static CommentDtos toCommentDto(this CommentEntity cmtModel)
        {
            return new CommentDtos
            {
                Id = cmtModel.Id,
                Title = cmtModel.Title,
                Content = cmtModel.Content,
                CreatedOn = cmtModel.CreatedOn,
                StockId = cmtModel.StockId,
            };
        }

        public static CommentEntity ToCommentFromCreateSanPham(this CreateCommentSanphamDtos commentDto, int StockId)
        {
            return new CommentEntity
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = StockId,
            };
        }

        public static CommentEntity ToCommentFromCreate(this CreateCommentDtos commentDto)
        {
            return new CommentEntity
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = (int)commentDto.StockId,
            };
        }

        public static CommentEntity ToCommentFromUpdate(this UpdateCommentDtos commentDto)
        {
            return new CommentEntity
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
        }

    }
}

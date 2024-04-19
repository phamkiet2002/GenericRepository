using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.CommentDto;
using WebTutorial.Helper;

namespace WebTutorial.Repository.Comment
{
    public interface ICommentRepository
    {
        Task<List<CommentEntity>> GetAll();
        Task<CommentEntity?> GettById(int id);
        Task<CommentEntity?> Update(int id, CommentEntity commentEntity);
        Task<CommentEntity> Create(CommentEntity comment);
        Task<CommentEntity?> Delete(int id);
    }
}

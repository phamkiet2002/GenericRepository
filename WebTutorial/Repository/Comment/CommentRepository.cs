using Microsoft.EntityFrameworkCore;
using WebAPI_Tutorial.Data;
using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.CommentDto;
using WebTutorial.Helper;

namespace WebTutorial.Repository.Comment
{
    public class CommentRepository : ICommentRepository

    {
        private readonly ApplicationDBContext _dbContext;
        public CommentRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<CommentEntity?> Create(CommentEntity comment)
        {
            await _dbContext.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<CommentEntity?> Delete(int id)
        {
            var cmt = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (cmt == null)
            {
                return null;
            }
            _dbContext.Remove(cmt);
            await _dbContext.SaveChangesAsync();
            return cmt;
        }

        public async Task<List<CommentEntity>> GetAll()
        {
            return await _dbContext.Comments.Include(c => c.Stock).ToListAsync();
        }

        public async Task<CommentEntity?> GettById(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }
        public Task<CommentEntity?> Update(int id, CommentDtos commentDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CommentEntity?> Update(int id, CommentEntity commentEntity)
        {
            var cmt = await _dbContext.Comments.FindAsync(id);
            if (cmt == null)
                return null;
            cmt.Title = commentEntity.Title;
            cmt.Content = commentEntity.Content;
            await _dbContext.SaveChangesAsync();
            return cmt;
        }
    }
}

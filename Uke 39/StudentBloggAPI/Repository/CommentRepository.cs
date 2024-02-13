using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly StudentBloggDbContext _dbContext;
        public CommentRepository(StudentBloggDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            var nyComment = await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return nyComment.Entity;
        }

        public async Task<Comment?> DeleteCommentByIdAsync(int id)
        {
            var CommentToDelete = await GetCommentByIdAsync(id);
            if (CommentToDelete != null)
            {
                _dbContext.Remove(CommentToDelete);
                await _dbContext.SaveChangesAsync();

                return CommentToDelete;
            }
            return null;
        }

        public Task<Comment?> GetCommentByIdAsync(int id)
        {
            return _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Comment>> GetPagedCommentsAsync(int pageNr, int pageSize)
        {
            var count = await _dbContext.Comments.CountAsync();
            if(count == 0)
                return Enumerable.Empty<Comment>().ToList();

            var comments = await _dbContext.Comments
                .OrderBy(x => x.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return comments;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment comment)
        {
            var commentToUpdate = await _dbContext.Comments.FindAsync(id);

            if (commentToUpdate != null && comment != null) 
            {
                commentToUpdate.Content = comment.Content;
                commentToUpdate.DateCommented = comment.DateCommented;
                commentToUpdate.Updated = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return commentToUpdate;
            }
            return null;
        }
    }
}

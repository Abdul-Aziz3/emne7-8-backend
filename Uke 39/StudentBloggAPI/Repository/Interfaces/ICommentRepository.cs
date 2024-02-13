using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces
{
    public interface ICommentRepository
    {
        // read
        Task<ICollection<Comment>> GetPagedCommentsAsync(int pageNr, int pageSize);
        Task<Comment?> GetCommentByIdAsync(int id);

        // create
        Task<Comment> AddCommentAsync(Comment comment);

        // update
        Task<Comment?> UpdateCommentAsync(int id, Comment comment);

        // delete
        Task<Comment?> DeleteCommentByIdAsync(int id);

    }
}

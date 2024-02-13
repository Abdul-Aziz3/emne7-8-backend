using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces
{
    public interface ICommentService
    {
        // read
        Task<ICollection<CommentDTO>> GetAllCommentsAsync(int pageNr, int pageSize);

        // create
        Task<CommentDTO?> AddCommentAsync(int postId, CommentDTO commentDTO);

        // update
        Task<CommentDTO?> UpdateCommentAsync(int id, CommentDTO commentDTO);

        // delete
        Task<CommentDTO?> DeleteCommentAsync(int id);

        Task<bool> UserOwnsCommentAsync(int commentId, int userId);
    }
}

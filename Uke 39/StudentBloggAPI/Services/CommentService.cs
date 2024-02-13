using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper<Comment, CommentDTO> _commentMapper;

        public CommentService(ICommentRepository commentRepository, IMapper<Comment, CommentDTO> commentMapper)
        {
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
        }
        public async Task<CommentDTO?> AddCommentAsync(int postId, CommentDTO commentDTO)
        {
            var comment = _commentMapper.MapToModel(commentDTO);
            comment.PostId = postId;

            var res = await _commentRepository.AddCommentAsync(comment);
            return res != null ? _commentMapper.MapToDTO(res) : null;
        }

        public async Task<CommentDTO?> DeleteCommentAsync(int id)
        {
            var res = await _commentRepository.DeleteCommentByIdAsync(id);

            return res != null ? _commentMapper.MapToDTO(res) : null;
        }

        public async Task<ICollection<CommentDTO>> GetAllCommentsAsync(int pageNr, int pageSize)
        {
            var comments = await _commentRepository.GetPagedCommentsAsync(pageNr, pageSize);

            return comments.Select(comment => _commentMapper.MapToDTO(comment)).ToList();
        }

        public async Task<CommentDTO?> UpdateCommentAsync(int id, CommentDTO commentDTO)
        {
            var comment = _commentMapper.MapToModel(commentDTO);
            var updatedComment = await _commentRepository.UpdateCommentAsync(id, comment);

            return updatedComment != null ? _commentMapper.MapToDTO(updatedComment) : null;
        }

        private object HttpContext()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserOwnsCommentAsync(int commentId, int userId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            return comment != null && comment.UserId == userId;
        }
    }
}

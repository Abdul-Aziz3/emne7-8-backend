using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces;

public interface IPostService
{
    // read
    Task<ICollection<PostDTO>> GetAllPostsAsync(int PageNr, int pageSize);
    Task<PostDTO?> GetPostByIdAsync(int id);

    // create
    Task<PostDTO?> AddPostAsync(PostDTO postDTO);

    // update
    Task<PostDTO?> UpdatePostAsync(int id, PostDTO postDTO);

    // delete
    Task<PostDTO?> DeletePostAsync(int id);

    Task<bool> UserOwnsPostAsync(int postId, int userId);
}

using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces;

public interface IPostRepository
{
    // create
    Task<Post> AddPostAsync(Post post);

    // update
    Task<Post?> UpdatePostAsync(int id, Post post);

    // delete
    Task<Post?> DeletePostByIdAsync(int id);

    // read
    Task<ICollection<Post>> GetPagedPostsAsync(int pageNr, int pageSize);

    Task<Post?> GetPostByIdAsync(int id);
}

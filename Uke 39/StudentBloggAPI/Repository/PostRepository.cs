using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository;

public class PostRepository : IPostRepository
{
    private readonly StudentBloggDbContext _dbContext;

    public PostRepository(StudentBloggDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Post> AddPostAsync(Post post)
    {
        var nyPost = await _dbContext.Posts.AddAsync(post);
        _dbContext.SaveChanges();
        return nyPost.Entity;
    }

    public async Task<Post?> DeletePostByIdAsync(int id)
    {
        var PostToDelete = await GetPostByIdAsync(id);
        if (PostToDelete != null)
        {
            _dbContext.Remove(PostToDelete);
            await _dbContext.SaveChangesAsync();

            return PostToDelete;
        }
        return null;
    }

    public async Task<ICollection<Post>> GetPagedPostsAsync(int pageNr, int pageSize)
    {
        var count = await _dbContext.Posts.CountAsync();
        if (count == 0)
            return Enumerable.Empty<Post>().ToList();

        var posts = await _dbContext.Posts
            .OrderBy(x => x.Id)
            .Skip((pageNr - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return posts;
    }

    public Task<Post?> GetPostByIdAsync(int id)
    {
        return _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post?> UpdatePostAsync(int id, Post post)
    {
        var postToUpdate = await _dbContext.Posts.FindAsync(id);

        if(postToUpdate != null && post != null)
        {
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            postToUpdate.Updated = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return postToUpdate;
        }
        return null;

    }
}

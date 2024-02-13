using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services;

public class PostService : IPostService
{
    private readonly IMapper<Post, PostDTO> _postMapper;
    private readonly IPostRepository _postRepository;

    public PostService(IMapper<Post, PostDTO> postMapper, IPostRepository postRepository)
    {
        _postMapper = postMapper;
        _postRepository = postRepository;
    }


    public async Task<PostDTO?> AddPostAsync(PostDTO postDTO)
    {
        var post = _postMapper.MapToModel(postDTO);

        var res = await _postRepository.AddPostAsync(post);
        return res != null ? _postMapper.MapToDTO(res) : null;

    }

    public async Task<PostDTO?> DeletePostAsync(int id)
    {
        var res = await _postRepository.DeletePostByIdAsync(id);
        return res != null ? _postMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<PostDTO>> GetAllPostsAsync(int pageNr, int pageSize)
    {
        var posts = await _postRepository.GetPagedPostsAsync(pageNr, pageSize);

        return posts.Select(post => _postMapper.MapToDTO(post)).ToList();
    }

    public async Task<PostDTO?> GetPostByIdAsync(int id)
    {
        var res = await _postRepository.GetPostByIdAsync(id);
        return res != null ? _postMapper.MapToDTO(res) : null;
    }

    public async Task<PostDTO?> UpdatePostAsync(int id, PostDTO postDTO)
    {
        var post = _postMapper.MapToModel(postDTO);
        var updatedPost = await _postRepository.UpdatePostAsync(id, post);

        return updatedPost != null ? _postMapper.MapToDTO(updatedPost) : null;
    }

    public async Task<bool> UserOwnsPostAsync(int postId, int userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);

        return post != null ? post.UserId == userId : false;
    }
}

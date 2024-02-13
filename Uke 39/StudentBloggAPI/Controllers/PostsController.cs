using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Services;
using StudentBloggAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    public PostsController(IPostService postService)
    {
        _postService = postService;
    }
    // GET: api/<PostController>
    [HttpGet(Name = "GetPosts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts(int PageNr = 1, int PageSize = 10)
    {
        var res = await _postService.GetAllPostsAsync(PageNr, PageSize);
        return res != null
            ? Ok(res)
            : NoContent();
    }


    // GET api/<PostController>/5
    [HttpGet("{postId}", Name = "GetPostsId")]
    public async Task<ActionResult<PostDTO>> GetPostByIdAsync(int postId)
    {
        var rest = await _postService.GetPostByIdAsync(postId);
        return rest != null ? Ok(rest) : NotFound("Fant ikke post");
    }

    // POST api/<PostController>
    [HttpPost(Name = "AddPost")]
    public async Task<ActionResult<PostDTO>> AddPostAsync(PostDTO postDTO)
    {
        int autenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

        if (postDTO.UserId != autenticatedUserId)
        {
            return Unauthorized("Du har ikke tilgang til å legge til innlegg på denne brukeren.");
        }

        var post = await _postService.AddPostAsync(postDTO);
        if (post == null)
            return BadRequest("Klarte ikke å legge til nytt innlegg.");

        return Ok(post);

    }

    // PUT api/<PostController>/5
    [HttpPut("{postId}", Name = "UpdatePost")]
    public async Task<ActionResult<PostDTO>> UpdatePostAsync(int postId, PostDTO postDTO)
    {
        int authenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

        if (await _postService.UserOwnsPostAsync(postId, authenticatedUserId))
        {
            var res = await _postService.UpdatePostAsync(postId, postDTO);
            return res != null ? Ok(res) : NotFound("Fikk ikke oppdatert innlegget eller innlegget eksisterer ikke");
        }

        return Unauthorized("Du har ikke tilgang til å oppdatere denne posten.");
    }

    // DELETE api/<PostController>/5
    [HttpDelete("{postId}", Name = "DeletePosts")]
    public async Task<ActionResult<PostDTO>> DeletePostAsync(int postId)
    {
        // Hent den autentiserte brukerens ID fra HttpContext
        int authenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

        if (await _postService.UserOwnsPostAsync(postId, authenticatedUserId))
        {
            var rest = await _postService.DeletePostAsync(postId);
            return rest != null ? Ok(rest) : NotFound("Fikk ikke slettet innlegget eller innlegget eksisterer ikke");
        }

        return Unauthorized("Du har ikke tilgang til å slette dette innlegget.");
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Services;
using StudentBloggAPI.Services.Interfaces;
using System.ComponentModel.Design;

namespace StudentBloggAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet(Name = "GetComments")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsAsync(int page = 1, int size = 10)
        {
            var res = await _commentService.GetAllCommentsAsync(page, size);
            return res != null
                ? Ok(res)
                : NoContent();
        }

        [HttpPost("{postId}", Name = "AddComment")]
        public async Task<ActionResult<CommentDTO>> AddCommentToPostAsync(int postId, CommentDTO commentDTO)
        {
            int autenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

            if(commentDTO.UserId !=  autenticatedUserId)
            {
                return Unauthorized("Du har ikke tilgang til å legge til kommentar på denne brukeren.");
            }

            var comment = await _commentService.AddCommentAsync(postId, commentDTO);
            if (comment == null)
                return BadRequest("Klarte ikke å legge til nytt kommentar.");

            return Ok(comment);

        }

        [HttpPut("{CommentId}", Name = "UpdateComment")]
        public async Task<ActionResult<CommentDTO>> UpdateCommentAsync(int CommentId, CommentDTO dto)
        {
            int authenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

            if (!await _commentService.UserOwnsCommentAsync(CommentId, authenticatedUserId))
            {
                return Unauthorized("Du har ikke tilgang til å oppdatere kommentaren");
            }

            var res = await _commentService.UpdateCommentAsync(CommentId, dto);
            return res != null ? Ok(res) : NotFound("Fikk ikke oppdatert kommentaren eller kommentaren eksistere ikke");
        }

        [HttpDelete("{CommentId}", Name = "DeleteComment")]
        public async Task<ActionResult<CommentDTO>> DeleteCommentAsync(int CommentId)
        {
            int authenticatedUserId = (int)this.HttpContext.Items["UserId"]!;

            if (!await _commentService.UserOwnsCommentAsync(CommentId, authenticatedUserId))
            {
                return Unauthorized("Du har ikke tilgang til å slette kommentaren");
            }

            var rest = await _commentService.DeleteCommentAsync(CommentId);
            return rest != null ? Ok(rest) : BadRequest("Fikk ikke slettet kommentaren eller kommentaren eksisterer ikke");

        }


    }
}

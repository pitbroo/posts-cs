using Microsoft.AspNetCore.Mvc;
using posts_cs.model;
using posts_cs.Services;
using WebApi.Models;

namespace posts_cs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentsController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        // GET: comments
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();
            return Ok(comments);
        }

        // GET: comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // POST: comments/postId
        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] CommentDto commentDto)
        {
     
            try
            {
                var comment = new Comment
                {
                    Text = commentDto.Text,
                    UserId = commentDto.UserId,
                };

                var createdComment = await _commentService.CreateComment(postId, comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.Id }, createdComment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            try
            {
                await _commentService.UpdateComment(id, comment);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await _commentService.DeleteComment(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

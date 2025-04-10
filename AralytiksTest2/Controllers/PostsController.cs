using AralytiksTest2.Contracts;
using AralytiksTest2.DTO;
using AralytiksTest2.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AralytiksTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService) =>
            this._postService = postService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return Problem("", "", (int)HttpStatusCode.BadRequest, "Page and pageSize must be positive numbers.", "https://tools.ietf.org/html/rfc9110#section-15.5.1");

            var (posts, totalCount) = await _postService.GetAllPostsAsync(page, pageSize);
            var response = new PaginatedResponse<Post>
            {
                Items = posts,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute][Required] int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"Post with Id = {id} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDto post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPost = await _postService.CreatePostAsync(post);
            if (createdPost == null)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"User with Id = {post.UserId} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");
            return CreatedAtAction(nameof(GetById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute][Required] int id, [FromBody] PostDto post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPost = await _postService.UpdatePostAsync(id, post);
            if (updatedPost == null)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"Post with Id = {id} or User with Id = {post.UserId} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");

            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await _postService.DeletePostAsync(id);
            if (!deleted)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"Post with Id = {id} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RedditClone.Services;
using RedditClone.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RedditClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] CreatePostDto post)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.AddPostAsync(post, userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.RemovePostAsync(id, userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var result = await _service.GetPostByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}

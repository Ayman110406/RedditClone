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
        private readonly IVoteService _voteService;

        public PostsController(IPostService service, IVoteService voteService)
        {
            _service = service;
            _voteService = voteService;
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
            Guid? userId = null;
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claim != null)
                userId = Guid.Parse(claim);

            var posts = await _service.GetAllPostsAsync(userId);
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

        [Authorize]
        [HttpPost("{id}/vote")]
        public async Task<IActionResult> Vote(Guid id, [FromBody] VoteDto vote)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _voteService.VoteOnPostAsync(id, userId, vote.Value);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}

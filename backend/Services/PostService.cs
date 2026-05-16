using RedditClone.Dtos;
using RedditClone.Models;
using RedditClone.Repositorys;

namespace RedditClone.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IVoteRepository _voteRepository;

        public PostService(IPostRepository repository, IVoteRepository voteRepository)
        {
            _repository = repository;
            _voteRepository = voteRepository;
        }

        // Post toevoegen
        public async Task<ServiceResult<object>> AddPostAsync(CreatePostDto post, Guid userId) {
            var newPost = new Post(userId, post.Title, post.Body);

            await _repository.AddPostAsync(newPost);
            await _repository.SaveChangesAsync();

            return new ServiceResult<object>()
            {
                Success = true,
            };
        }

        // Post verwijderen
        public async Task<ServiceResult<object>> RemovePostAsync(Guid id, Guid userId) { 
            var controleerPost = await _repository.GetByIdAsync(id);

            if (controleerPost == null)
                return new ServiceResult<object> { Success = false, ErrorMessage = "Post niet gevonden" };

            if (controleerPost.UserId != userId)
                return new ServiceResult<object> { Success = false, ErrorMessage = "Je hebt geen toegang tot deze post" };


            await _repository.DeletePostAsync(id);
            await _repository.SaveChangesAsync();
            return new ServiceResult<object> { Success = true };
        }

        // Alle posts opvragen
        public async Task<ServiceResult<List<PostDto>>> GetAllPostsAsync(Guid? userId = null) {
            var allePosts = await _repository.GetAllAsync();

            var userVoteMap = new Dictionary<Guid, int>();
            if (userId.HasValue)
            {
                var userVotes = await _voteRepository.GetVotesByUserAsync(userId.Value, TargetType.Post);
                userVoteMap = userVotes.ToDictionary(v => v.TargetId, v => v.Value);
            }

            var postDtos = allePosts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                Username = p.Author.Username,
                VoteScore = p.VoteScore,
                CommentCount = p.Comments.Count,
                UserVote = userVoteMap.GetValueOrDefault(p.Id, 0),
                CreatedAt = p.CreatedAt
            }).ToList();


            return new ServiceResult<List<PostDto>> { 
                Success = true, 
                Data = postDtos 
            };
        }

        // Post details opvragen
        public async Task<ServiceResult<PostDetailDto>> GetPostByIdAsync(Guid id) {
            var post = await _repository.GetByIdAsync(id);

            if (post == null){
                return new ServiceResult<PostDetailDto> { Success = false, ErrorMessage = "Post niet gevonden" };
            }

            var postDto = new PostDetailDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Username = post.Author.Username,
                VoteScore = post.VoteScore,
                CreatedAt = post.CreatedAt,
                Comments = post.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Body = c.Body,
                    Username = c.Author.Username,
                    VoteScore = c.VoteScore,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return new ServiceResult<PostDetailDto> { Success = true, Data = postDto };
        }

    }
}

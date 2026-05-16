using RedditClone.Dtos;
using RedditClone.Models;
using RedditClone.Repositorys;

namespace RedditClone.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPostRepository _postRepository;

        public VoteService(IVoteRepository voteRepository, IPostRepository postRepository)
        {
            _voteRepository = voteRepository;
            _postRepository = postRepository;
        }

        // Stemmen op een post
        public async Task<ServiceResult<VoteResultDto>> VoteOnPostAsync(Guid postId, Guid userId, int value)
        {
            if (value != 1 && value != -1)
                return new ServiceResult<VoteResultDto> { Success = false, ErrorMessage = "Waarde moet 1 of -1 zijn" };

            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                return new ServiceResult<VoteResultDto> { Success = false, ErrorMessage = "Post niet gevonden" };

            var bestaandeVote = await _voteRepository.GetVoteAsync(userId, postId, TargetType.Post);

            int delta;
            int userVote;

            if (bestaandeVote != null)
            {
                if (bestaandeVote.Value == value)
                {
                    delta = -value;
                    _voteRepository.RemoveVote(bestaandeVote);
                    userVote = 0;
                }
                else
                {
                    delta = value * 2;
                    bestaandeVote.Value = value;
                    userVote = value;
                }
            }
            else
            {
                var nieuwVote = new Vote(userId, postId, TargetType.Post, value);
                await _voteRepository.AddVoteAsync(nieuwVote);
                delta = value;
                userVote = value;
            }

            post.VoteScore += delta;
            await _voteRepository.SaveChangesAsync();

            return new ServiceResult<VoteResultDto>
            {
                Success = true,
                Data = new VoteResultDto
                {
                    NewScore = post.VoteScore,
                    UserVote = userVote
                }
            };
        }
    }
}

using RedditClone.Dtos;

namespace RedditClone.Services
{
    public interface IVoteService
    {
        Task<ServiceResult<VoteResultDto>> VoteOnPostAsync(Guid postId, Guid userId, int value);
    }
}

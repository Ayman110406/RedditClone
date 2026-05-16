using RedditClone.Models;

namespace RedditClone.Repositorys
{
    public interface IVoteRepository
    {
        Task<Vote?> GetVoteAsync(Guid userId, Guid targetId, TargetType targetType);
        Task<List<Vote>> GetVotesByUserAsync(Guid userId, TargetType targetType);
        Task AddVoteAsync(Vote vote);
        void RemoveVote(Vote vote);
        Task SaveChangesAsync();
    }
}

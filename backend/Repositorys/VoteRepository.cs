using Microsoft.EntityFrameworkCore;
using RedditClone.Data;
using RedditClone.Models;

namespace RedditClone.Repositorys
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Haal een specifieke stem op 
        public async Task<Vote?> GetVoteAsync(Guid userId, Guid targetId, TargetType targetType)
        {
            return await _context.Votes
                .FirstOrDefaultAsync(v => v.UserId == userId && v.TargetId == targetId && v.TargetType == targetType);
        }

        // Haal alle stemmen van een gebruiken op een post of vote
        public async Task<List<Vote>> GetVotesByUserAsync(Guid userId, TargetType targetType)
        {
            return await _context.Votes
                .Where(v => v.UserId == userId && v.TargetType == targetType)
                .ToListAsync();
        }

        // Haal alle stemmen op een post of comment op
        public async Task AddVoteAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
        }

        // Verwijder een stem
        public void RemoveVote(Vote vote)
        {
            _context.Votes.Remove(vote);
        }

        // Sla de wijzigingen op in de database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

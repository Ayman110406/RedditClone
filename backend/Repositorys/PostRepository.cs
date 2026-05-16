using Microsoft.EntityFrameworkCore;
using RedditClone.Models;
using RedditClone.Data;

namespace RedditClone.Repositorys
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        // Post ophalen op basis van id
        public async Task<Post?> GetByIdAsync(Guid id) {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                //.Include(p => p.Votes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Alle posts ophalen
        public async Task<List<Post>> GetAllAsync() {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .Include(p => p.Votes)
                .ToListAsync();
        }

        // Nieuwe post toevoegen
        public async Task AddPostAsync(Post post) { 
            await _context.Posts.AddAsync(post);
        }

        // Post verwijderen op basis van id
        public async Task DeletePostAsync(Guid id) {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
                _context.Posts.Remove(post);
        }

        // Veranderingen opslaan in de database
        public async Task SaveChangesAsync() { 
            await _context.SaveChangesAsync();
        }
    }
}

using RedditClone.Data;
using RedditClone.Models;
using Microsoft.EntityFrameworkCore;

namespace RedditClone.Repositorys
{
    public class UserRepository : IUserRepository
    {

       private readonly AppDbContext _context;

       public UserRepository(AppDbContext context) {
            _context = context;
        }

        // Gebruiker ophalen op basis van email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Gebruiker ophalen op basis van id
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Controleren of email al bestaat
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        // Controleren of username al bestaat
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        // Nieuwe gebruiker toevoegen aan de database
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        // Wijzigingen opslaan in de database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}

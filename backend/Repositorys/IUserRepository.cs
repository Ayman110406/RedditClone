using RedditClone.Models;

namespace RedditClone.Repositorys
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();

    }
}

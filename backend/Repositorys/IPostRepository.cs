using RedditClone.Models;

namespace RedditClone.Repositorys
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id);
        Task<List<Post>> GetAllAsync();
        Task AddPostAsync(Post post);
        Task DeletePostAsync(Guid id);
        Task SaveChangesAsync();

    }
}

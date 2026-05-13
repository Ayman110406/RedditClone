using RedditClone.Models;

namespace RedditClone.Services
{
    public interface ITokenService
    {

        string GenerateAccessToken(User user);

    }
}

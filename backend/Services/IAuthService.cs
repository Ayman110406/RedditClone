using RedditClone.Dtos;

namespace RedditClone.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginUserAsync(LoginDto dto);
    }
}

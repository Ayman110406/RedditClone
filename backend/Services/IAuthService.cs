using RedditClone.Dtos;

namespace RedditClone.Services
{
    public interface IAuthService
    {
        Task<ServiceResult<object>> RegisterUserAsync(RegisterDto dto);
        Task<ServiceResult<AuthResponseDto>> LoginUserAsync(LoginDto dto);
    }
}

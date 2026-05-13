using RedditClone.Dtos;
using RedditClone.Models;
using RedditClone.Repositorys;
using System.Text.RegularExpressions;

namespace RedditClone.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenservice;

        public AuthService(IUserRepository userRepository, ITokenService tokenservice)
        {
            _repository = userRepository;
            _tokenservice = tokenservice;
        }

        // Registreren van gebruiker
        public async Task<ServiceResult<object>> RegisterUserAsync(RegisterDto dto)
        {
            // Controleren of email al bestaat
            if (await _repository.EmailExistsAsync(dto.Email) )
                return new ServiceResult<object> { Success = false, ErrorMessage = "Email bestaat al" };

            // Controleren of username al bestaat
            if (await _repository.UsernameExistsAsync(dto.Username))
                return new ServiceResult<object> { Success = false, ErrorMessage = "Gebruikersnaam bestaat al" };

            // Nieuwe gebruiker aanmaken en opslaan
            var user = new User(dto.Username, dto.Email, HashPassword(dto.Password));

            // Naar de database sturen en opslaan
            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();

            return new ServiceResult<object> { Success = true };
        }

        // Inloggen van gebruiker
        public async Task<ServiceResult<AuthResponseDto>> LoginUserAsync(LoginDto dto)
        {
            // Gebruiker ophalen en wachtwoord controleren
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return new ServiceResult<AuthResponseDto> { Success = false, ErrorMessage = "Ongeldige inloggegevens" };

            // Token teruggeven aan de frontend
            return new ServiceResult<AuthResponseDto>
            {
                Success = true,
                Data = new AuthResponseDto
                { 
                    Username = user.Username,
                    AccessToken = _tokenservice.GenerateAccessToken(user)
                }
            };
        }

        // Wachtwoord hashen
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Wachtwoord verifiëren
        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
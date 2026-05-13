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
        public async Task<bool> RegisterUserAsync(RegisterDto dto)
        {
            // Controleren of email en username al bestaat
            if (await _repository.EmailExistsAsync(dto.Email) || await _repository.UsernameExistsAsync(dto.Username))
            {
                return false;
            }

            // Wachtwoordlengte & emailformaat controleren
            if (dto.Password.Length < 6 || !IsValidEmail(dto.Email))
            {
                return false;
            }

            // Nieuwe gebruiker aanmaken en opslaan
            var user = new User(dto.Username, dto.Email, HashPassword(dto.Password));

            // Naar de database sturen en opslaan
            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();
            return true;
        }

        // Inloggen van gebruiker
        public async Task<AuthResponseDto?> LoginUserAsync(LoginDto dto)
        {
            // Gebruiker ophalen en wachtwoord controleren
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
            {
                return null;
            }

            // Token teruggeven aan de frontend
            return new AuthResponseDto
            {
                Username = user.Username,
                AccessToken = _tokenservice.GenerateAccessToken(user) 
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

        // Email validatie
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
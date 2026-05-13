using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace RedditClone.Models
{
    public class User
    {

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Vote> Votes { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public User(string username, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            Posts = new List<Post>();
            Comments = new List<Comment>();
            Votes = new List<Vote>();
            RefreshTokens = new List<RefreshToken>();


        }
    }
}

using System.Xml.Linq;

namespace RedditClone.Models
{
    public class Post
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int VoteScore { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Author { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Vote> Votes { get; set; }

        public Post(Guid userId, string title, string body)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Body = body;
            VoteScore = 0;
            CreatedAt = DateTime.UtcNow;
            Comments = new List<Comment>();
            Votes = new List<Vote>();
        }

    }
}

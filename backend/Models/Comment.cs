namespace RedditClone.Models
{
    public class Comment
    {

        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }
        public string Body { get; set; }
        public int VoteScore { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Author { get; set; }
        public Post Post { get; set; }
        public Comment? Parent { get; set; }
        public List<Comment> Replies { get; set; }
        public List<Vote> Votes { get; set; }

        public Comment(Guid postId, Guid userId, string body, Guid? parentId = null)
        {
            Id = Guid.NewGuid();
            PostId = postId;
            UserId = userId;
            Body = body;
            ParentId = parentId;
            VoteScore = 0;
            CreatedAt = DateTime.UtcNow;
            Replies = new List<Comment>();
            Votes = new List<Vote>();
        }

    }
}

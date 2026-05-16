namespace RedditClone.Dtos
{
    public class PostDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public int VoteScore { get; set; }
        public int CommentCount { get; set; }
        public int UserVote { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

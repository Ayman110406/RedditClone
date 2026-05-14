namespace RedditClone.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
        public int VoteScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentDto> Replies { get; set; }

    }
}

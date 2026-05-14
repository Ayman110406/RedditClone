namespace RedditClone.Dtos
{
    public class PostDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public int VoteScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}

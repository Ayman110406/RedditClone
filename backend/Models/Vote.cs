namespace RedditClone.Models
{
    public class Vote
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public Vote(Guid userId, Guid targetId, TargetType targetType, int value)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            TargetId = targetId;
            TargetType = targetType;
            Value = value;
            CreatedAt = DateTime.UtcNow;
        }

    }
}

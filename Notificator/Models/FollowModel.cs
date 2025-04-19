namespace Notificator.Models;

public class FollowModel
{
    public class Follow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ChannelId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public Follow(int id, int userId, string channelId, DateTime createdAt, DateTime? updateAt)
        {
            Id = id;
            UserId = userId;
            ChannelId = channelId;
            CreatedAt = createdAt;
            UpdateAt = updateAt;
        }
    }
    public class FollowRq
    {
        public int UserId { get; set; }
        public string ChannelId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
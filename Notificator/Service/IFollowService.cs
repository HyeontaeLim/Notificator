using static Notificator.Models.FollowModel;
using static Notificator.Models.ReturnCodeModel;

namespace Notificator.Service;

public interface IFollowService
{
    public bool IsFollowing(int userId, string channelId);
    public ReturnRs ToggleFollow(int userId, string channelId);
}
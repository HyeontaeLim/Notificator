using static Notificator.Models.FollowModel;
using static Notificator.Models.ReturnCodeModel;

namespace Notificator.Repository;

public interface IFollowRepository
{
    public ReturnRs AddFollow(FollowRq followRq);
    public bool IsFollowing(int userId, string channelId);
    public ReturnRs RemoveFollow(int userId, string channelId);
    public List<Follow> GetAllFollowList();
}
using Notificator.Models;
using Notificator.Repository;
using static Notificator.Models.FollowModel;

namespace Notificator.Service;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;

    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public ReturnCodeModel.ReturnRs AddFollow(FollowRq followRq)
    {
        return _followRepository.AddFollow(followRq);
    }

    public bool IsFollowing(int userId, string channelId)
    {
        return _followRepository.IsFollowing(userId, channelId);
    }

    public ReturnCodeModel.ReturnRs RemoveFollow(int userId, string channelId)
    {
        return _followRepository.RemoveFollow(userId, channelId);
    }

    public ReturnCodeModel.ReturnRs ToggleFollow(int userId, string channelId)
    {
        if (_followRepository.IsFollowing(userId, channelId))
        {
            return _followRepository.RemoveFollow(userId, channelId);
        }
        else
        {
            return _followRepository.AddFollow(new FollowRq { UserId = userId, ChannelId = channelId });
        }
    }
}
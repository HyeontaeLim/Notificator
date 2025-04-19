using Notificator.ApiSend;
using Notificator.Repository;
using Notificator.Service;
using static Notificator.Models.ChzzkModel;
using static Notificator.Models.FollowModel;
using static Notificator.Models.UsersModel;

public class StreamMonitorService : BackgroundService
{
    private readonly ILogger<StreamMonitorService> _logger;
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly IFollowRepository _followRepository;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);
    private readonly HashSet<string> _liveSet = new HashSet<string>();
    private readonly HashSet<string> _tempLiveSet = new HashSet<string>();

    public StreamMonitorService(ILogger<StreamMonitorService> logger, IConfiguration config, IUserRepository userRepository, IFollowRepository followRepository)
    {
        _logger = logger;
        _config = config;
        _userRepository = userRepository;
        _followRepository = followRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("스트림 모니터링 서비스 시작됨");
        ChzzkApiSend chzzkApiSend = new ChzzkApiSend(_config);
        DiscordApiSend discordApiSend = new DiscordApiSend(_config);

        while (!stoppingToken.IsCancellationRequested)
        {
            List<Follow> followList = _followRepository.GetAllFollowList();
            foreach (var follow in followList)
            {
                string channelId = follow.ChannelId;
                LiveInfoRs? liveInfoRs = await chzzkApiSend.getLiveInfo(channelId);

                if (liveInfoRs.content.openLive && !_liveSet.Contains(channelId))
                {
                    _tempLiveSet.Add(liveInfoRs.content.channelId);
                    User? user = _userRepository.FindUsersById(follow.UserId);
                    string content =
                        $"{liveInfoRs.content.channelName}님이 방송을 시작하셨습니다.\nhttps://chzzk.naver.com/live/{liveInfoRs.content.channelId}";
                    await discordApiSend.sendMessage(content, user.UserId, null ,user.DiscordWebhookUrl );
                }
                else
                {
                    _liveSet.Remove(liveInfoRs.content.channelId);
                }
            }
            _liveSet.UnionWith(_tempLiveSet);
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }
}

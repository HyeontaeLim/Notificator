using Microsoft.AspNetCore.Mvc;
using Notificator.Filter;
using Notificator.Service;
using static Notificator.Models.FollowModel;
using static Notificator.Models.ReturnCodeModel;
using static Notificator.Models.UsersModel;

namespace Notificator.Controllers;

public class FollowController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserService _userService;
    private readonly IFollowService _followService;


    public FollowController(ILogger<LoginController> logger, IUserService userService, IFollowService followService)
    {
        _logger = logger;
        _userService = userService;
        _followService = followService;
    }

    [LoginFilter]
    [Route("/Follow/ToggleFollow")]
    [HttpPost]
    public IActionResult ToggleFollow(string channelId)
    {
        int loginUserId = int.Parse(HttpContext.Session.GetString("LoginSession"));
        ReturnRs returnRs = _followService.ToggleFollow(loginUserId, channelId);
        return Json(returnRs);
    }

    [LoginFilter]
    [Route("/Follow/IsFollowing")]
    [HttpGet]
    public IActionResult IsFollowing(string channelId)
    {
        int loginUserId = int.Parse(HttpContext.Session.GetString("LoginSession"));
        bool isFollowing = _followService.IsFollowing(loginUserId, channelId);
        return Json(isFollowing);
    }
}

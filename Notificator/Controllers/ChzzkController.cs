using Microsoft.AspNetCore.Mvc;
using Notificator.ApiSend;
using Notificator.Filter;
using static Notificator.ConstantKey;
using static Notificator.Models.ChzzkModel;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Notificator.Models;
using Notificator.Service;
using static Notificator.Models.UsersModel;

namespace Notificator.Controllers;

public class ChzzkController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public ChzzkController(ILogger<HomeController> logger, IConfiguration config, IUserService userService)
    {
        _logger = logger;
        _config = config;
        _userService = userService;
    }

    [Route("/getAccessToken")]
    public async Task<IActionResult> GetAccessToken(string code, string state)
    {
        ChzzkApiSend chzzkApiSend = new ChzzkApiSend(_config);
        AccessTokenRq accessToken = new AccessTokenRq();
        accessToken.grantType = "authorization_code";
        accessToken.code = code;
        accessToken.clientId = ChzzkConstant.clientId;
        accessToken.state = state;
        accessToken.clientSecret = ChzzkConstant.clientSecret;

        await chzzkApiSend.getAccessToken(accessToken);
        return Redirect("/");
    }

    [Route("/ChzzkLogin")]
    public IActionResult ChzzkLogin()
    {
        string host = _config["Chzzk:host"];
        string clientId = ChzzkConstant.clientId;
        string redirectUri = ChzzkConstant.redirectUri;
        string state = ChzzkConstant.state;
        string url = $"{host}/account-interlock" +
                     $"?clientId={clientId}" +
                     $"&redirectUri={Uri.EscapeDataString(redirectUri)}" +
                     $"&state={state}";
        return Redirect(url);
    }

    [LoginFilter]
    [Route("/chzzkSearch")]
    public async Task<IActionResult> ChzzkSearch(string keyword, int offset = 0, int size = 10)
    {
        ChzzkApiSend chzzkApiSend = new ChzzkApiSend(_config);
        ViewData["SearchResult"] = await chzzkApiSend.chzzkSearch(keyword, offset, size);
        return View("~/Views/Search/search.cshtml");
    }

    [LoginFilter]
    [Route("/getLiveList")]
    public async Task<IActionResult> GetLiveList(string next)
    {
        ChzzkApiSend chzzkApiSend = new ChzzkApiSend(_config);
        LiveListRs liveList = await chzzkApiSend.getLiveList(next);
        return ViewComponent("LiveList", liveList);
    }
}
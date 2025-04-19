using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Notificator.ApiSend;
using Notificator.Models;
using Notificator.Service;
using static Notificator.ConstantKey;

namespace Notificator.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public HomeController(ILogger<HomeController> logger, IConfiguration config, IUserService userService)
    {
        _logger = logger;
        _config = config;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        string? loginId = HttpContext.Session.GetString("LoginSession");
        if (string.IsNullOrEmpty(loginId))
        {
            return Redirect("/login");
        }

        ViewData["LiveList"] = null;
        if (ChzzkConstant.accessToken != null)
        {
            ChzzkApiSend chzzkApiSend = new ChzzkApiSend(_config);
            ViewData["LiveList"] = await chzzkApiSend.getLiveList("");
        }

        return View("~/Views/Home/Index.cshtml");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
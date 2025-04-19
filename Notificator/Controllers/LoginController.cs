using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notificator.ApiSend;
using Notificator.Service;
using static Notificator.Models.ReturnCodeModel;
using static Notificator.Models.UsersModel;

namespace Notificator.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;
    private readonly PasswordHasher<string> _passwordHasher;

    public LoginController(ILogger<LoginController> logger, IConfiguration config, IUserService userService, PasswordHasher<string> passwordHasher)
    {
        _logger = logger;
        _config = config;
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    [Route("/login")]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Login/Login.cshtml");
    }

    [Route("/login")]
    [HttpPost]
    public IActionResult Login(LoginRq loginRq, [FromQuery(Name = "redirectUrl")] string redirectUrl = "/")
    {
        _passwordHasher.HashPassword(loginRq.UserId, loginRq.Password);
        User? loginUser = _userService.LoginUser(loginRq);
        if (loginUser == null)
        {
            ModelState.AddModelError(String.Empty, "아이디 혹은 비밀번호가 일치하지 않습니다.");
        }

        if (!ModelState.IsValid)
        {
            return View("~/Views/Login/Login.cshtml", loginRq);
        }

        HttpContext.Session.SetString("LoginSession", loginUser.Id.ToString());

        return Redirect(redirectUrl);
    }

    [Route("/logout")]
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("/login");
    }
}
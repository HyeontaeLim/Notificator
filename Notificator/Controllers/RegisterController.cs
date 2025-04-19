using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notificator.Service;
using static Notificator.Models.ReturnCodeModel;
using static Notificator.Models.UsersModel;

namespace Notificator.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;
    private readonly IUserService _userService;
    private readonly PasswordHasher<string> _passwordHasher;

    public RegisterController(ILogger<RegisterController> logger, IUserService userService, PasswordHasher<string> passwordHasher)
    {
        _logger = logger;
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    [Route("/Register")]
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/Register/Register.cshtml");
    }

    [Route("/Register")]
    [HttpPost]
    public IActionResult PutUser(UsersRq usersRq)
    {
        string hashPassword = _passwordHasher.HashPassword(usersRq.UserId, usersRq.Password);
        string discordWebhookUrl = _passwordHasher.HashPassword(usersRq.UserId, usersRq.DiscordWebhookUrl ?? "");
        UsersRq rq = new UsersRq()
        {
            UserId = usersRq.UserId,
            Password = hashPassword,
            Email = usersRq.Email,
            Name = usersRq.Name,
            DiscordWebhookUrl = discordWebhookUrl,
            CreatedAt = DateTime.Now
        };
        ReturnRs returnRs = _userService.PutUser(rq);
        return Json(returnRs);
    }
}
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Notificator.Filter;

public class LoginFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        HttpRequest request = context.HttpContext.Request;
        string url = $"{request.Path}{request.QueryString}";
        string encodedUrl = HttpUtility.UrlEncode(url);
        string? loginedId = context.HttpContext.Session.GetString("LoginSession");
        if (string.IsNullOrEmpty(loginedId))
        {
            context.Result = new RedirectResult($"login?redirectUrl={encodedUrl}");
        }
    }
}
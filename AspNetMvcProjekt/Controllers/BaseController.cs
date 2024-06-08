using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers;

public abstract class BaseController(UserManager<User> userManager) : Controller
{
    public User? LoggedInUser { get; set; }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await Console.Out.WriteLineAsync("1");
        LoggedInUser = await userManager.GetUserAsync(User);
        await Console.Out.WriteLineAsync("2");
        ViewBag.IsLoggedIn = LoggedInUser != null;
        await Console.Out.WriteLineAsync("3");
        await base.OnActionExecutionAsync(context, next);
    }
}

using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetMvcProjekt.Web.Controllers;

public abstract class BaseController(UserManager<User> userManager) : Controller
{
    public User? LoggedInUser { get; set; }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        LoggedInUser = await userManager.GetUserAsync(User);
        await base.OnActionExecutionAsync(context, next);
    }
}

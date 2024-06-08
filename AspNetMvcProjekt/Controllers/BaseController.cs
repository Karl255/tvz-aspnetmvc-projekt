﻿using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers;

public abstract class BaseController(UserManager<User> userManager) : Controller
{
    public User? LoggedInUser { get; set; }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        LoggedInUser = await userManager.GetUserAsync(User);
        ViewBag.IsLoggedIn = LoggedInUser != null;
        await base.OnActionExecutionAsync(context, next);
    }
}

using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using AspNetMvcProjekt.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("dev")]
public class TestController(UserManager<User> userManager, StoreDbContext dbContext) : BaseController(userManager)
{
    [Route("no-auth")]
    public IActionResult IndexUnauthorized()
    {
        ViewBag.Message = "No auth required";
        return View("Index");
    }

    [Route("auth")]
    [Authorize]
    public IActionResult IndexAuthorized()
    {
        ViewBag.Message = "Auth required";
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("migrate")]
    public async Task<IActionResult> Migrate()
    {
        await dbContext.Database.MigrateAsync();
        return Ok(new { message = "Database migrated" });
    }
}

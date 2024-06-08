using AspNetMvcProjekt.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("dev")]
public class TestController : Controller
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
}

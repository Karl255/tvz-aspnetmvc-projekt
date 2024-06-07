using AspNetMvcProjekt.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetMvcProjekt.Web.Controllers;

public class TestController : Controller
{
    [ActionName("Unauthorized")]
    public IActionResult IndexUnauthorized()
    {
        ViewBag.Message = "Unauthorized";
        return View("Index");
    }

    [ActionName("Authorized")]
    public IActionResult IndexAuthorized()
    {
        ViewBag.Message = "Authorized";
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

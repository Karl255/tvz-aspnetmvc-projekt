using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class CategoryController : Controller
{
    [Route("/")]
    public IActionResult All()
    {
        return View();
    }

    public IActionResult Category(string name)
    {
        ViewBag.CategoryName = name;
        return View();
    }
}

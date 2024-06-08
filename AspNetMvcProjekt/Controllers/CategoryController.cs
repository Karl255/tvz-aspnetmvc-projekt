using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(UserManager<User> userManager) : base(userManager) { }

    [Route("/")]
    public IActionResult All()
    {
        Console.WriteLine("4");
        return View(GetMockItems());
    }

    public IActionResult Category(string name)
    {
        ViewBag.CategoryName = name;
        return View(GetMockItems());
    }

    private List<Item> GetMockItems() => [
        new() { Name = "Resistor 1k", Description = "1k E3", AmountInStorage = 2200, Price = 0.02M },
        new() { Name = "Resistor 2k2", Description = "2k2 E3", AmountInStorage = 1200, Price = 0.02M },
        new() { Name = "Resistor 4k7", Description = "4k7 E3", AmountInStorage = 200, Price = 0.02M },
    ];
}

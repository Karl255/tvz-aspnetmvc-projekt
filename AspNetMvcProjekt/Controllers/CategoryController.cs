using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;

public class CategoryController(
    UserManager<User> userManager,
    StoreDbContext dbContext
) : BaseController(userManager)
{
    [Route("/")]
    public IActionResult All()
    {
        return View(GetItems().ToList());
    }

    public IActionResult Category(string name)
    {
        ViewBag.CategoryName = name;
        var items = GetItems().ToList().Where(i => i.Category?.Name == name).ToList();
        return View(items);
    }

    private IQueryable<Item> GetItems() => dbContext.Items.Include(i => i.Category);
}

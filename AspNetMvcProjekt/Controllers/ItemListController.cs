using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcProjekt.Web.Controllers;

public class ItemListController(
    UserManager<User> userManager,
    StoreDbContext dbContext
) : BaseController(userManager)
{
    [Route("/")]
    public IActionResult Index()
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

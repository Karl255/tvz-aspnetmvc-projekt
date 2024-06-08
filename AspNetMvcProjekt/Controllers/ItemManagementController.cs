using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;

public class ItemManagementController(
    UserManager<User> userManager,
    StoreDbContext dbContext
) : BaseController(userManager)
{
    [Authorize]
    public IActionResult Index()
    {
        return View(GetItems().ToList());
    }

    [Authorize]
    public IActionResult Edit(int id)
    {
        Console.WriteLine($"Action: Edit id = {id}");
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult Delete(int id)
    {
        Console.WriteLine($"Action: Delete id = {id}");
        return RedirectToAction(nameof(Index));
    }

    private IQueryable<Item> GetItems() => dbContext.Items.Include(i => i.Category);
}

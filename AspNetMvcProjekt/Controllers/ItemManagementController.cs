using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ItemManagementController : BaseController
{
    public ItemManagementController(UserManager<User> userManager) : base(userManager) { }

    [Authorize]
    public IActionResult Index()
    {
        return View(new List<Item>());
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
}

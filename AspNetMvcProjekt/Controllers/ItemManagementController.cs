using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public IActionResult Create()
    {
        FillAvailableCategories();
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Create(Item item)
	{
		if (!ModelState.IsValid)
		{
            var errors = string.Join(",\n", ModelState.Values.SelectMany(value => value.Errors.Select(error => error.ErrorMessage)));
            Console.WriteLine(errors);
            return RedirectToAction(nameof(Create));
		}

		dbContext.Add(item);
		dbContext.SaveChanges();
		return RedirectToAction(nameof(Index));
	}

	[Authorize]
    public IActionResult Edit(int id)
    {
        Console.WriteLine($"Action: Edit id = {id}");
        FillAvailableCategories();
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult Delete(int id)
    {
        Console.WriteLine($"Action: Delete id = {id}");
        return RedirectToAction(nameof(Index));
    }

    private IQueryable<Item> GetItems() => dbContext.Items.Include(i => i.Category);

    private void FillAvailableCategories()
    {
        var categoryOptions = dbContext.Categories.Select(category => new SelectListItem(category.Name, category.Id.ToString())).ToList();
        ViewBag.CategoryOptions = categoryOptions;
    }
}

using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcProjekt.Web.Controllers;

[Authorize]
public class ItemManagementController(
	UserManager<User> userManager,
	StoreDbContext dbContext
) : BaseController(userManager)
{
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

	public IActionResult Edit(int id)
	{
		var item = dbContext.Items.FirstOrDefault(x => x.Id == id);

		if (item is null)
		{
			return NotFound();
		}

		FillAvailableCategories();
		return View(item);
	}

	[ActionName("Edit")]
	[HttpPost]
	public async Task<IActionResult> EditPost(int id)
	{
		var item = dbContext.Items.FirstOrDefault(i => i.Id == id);

		if (item is null)
		{
			return NotFound();
		}

		var hasSucceded = await TryUpdateModelAsync(item);

		if (hasSucceded && ModelState.IsValid)
		{
			dbContext.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		return RedirectToAction(nameof(Edit));
	}

	[HttpDelete]
	public IActionResult Delete(int id)
	{
        var itemToDelete = dbContext.Items.FirstOrDefault(c => c.Id == id);

        if (itemToDelete is null)
        {
            return NotFound();
        }

        dbContext.Items.Remove(itemToDelete);
        dbContext.SaveChanges();
        return NoContent();
    }

	private IQueryable<Item> GetItems() => dbContext.Items.Include(i => i.Category);

	private void FillAvailableCategories()
	{
		var categoryOptions = dbContext.Categories.Select(category => new SelectListItem(category.Name, category.Id.ToString())).ToList();
		ViewBag.CategoryOptions = categoryOptions;
	}
}

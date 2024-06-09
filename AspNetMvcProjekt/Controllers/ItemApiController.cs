using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("/api/items")]
public class ItemApiController(StoreDbContext dbContext) : ControllerBase
{
	public IActionResult GetAll()
	{
		var items = dbContext.Items.Include(i => i.Category).Select(item => item.ToDto()).ToList();
		return Ok(items);
	}

	[Route("{id}")]
	public IActionResult GetOne(int id)
	{
		var item = dbContext.Items.Include(i => i.Category).FirstOrDefault(item => item.Id == id)?.ToDto();
		return item != null ? Ok(item) : NotFound();
	}
}

public record ItemDto(
	int Id,
	string Name,
	string? Description,
	int AmountInStorage,
	decimal Price,
	string Category
);

internal static class Extensions
{
	internal static ItemDto ToDto(this Item item) => new(
		item.Id,
		item.Name,
		item.Description,
		item.AmountInStorage,
		item.Price,
		item.Category!.Name
	);
}

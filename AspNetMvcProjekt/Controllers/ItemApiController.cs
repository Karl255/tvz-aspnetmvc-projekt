using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("/api/items")]
public class ItemApiController(StoreDbContext dbContext) : ControllerBase
{
	[HttpGet]
	public IActionResult GetAll()
	{
		var items = dbContext.Items.Include(i => i.Category).Select(item => item.ToDto()).ToList();
		return Ok(items);
	}

	[HttpGet]
	[Route("{id}")]
	public IActionResult GetOne(int id)
	{
		var item = dbContext.Items.Include(i => i.Category).FirstOrDefault(item => item.Id == id)?.ToDto();
		return item != null ? Ok(item) : NotFound();
	}

	[HttpPost]
	public IActionResult Create([FromBody] ItemDto item)
	{
		var category = dbContext.Categories.FirstOrDefault(c => c.Name == item.Category);

		if (category == null)
		{
			return BadRequest(new { Message = $"Category with name {item.Category} does not exist" });
		}

		var itemEntity = new Item
		{
			Name = item.Name,
			Description = item.Description,
			AmountInStorage = item.AmountInStorage,
			Price = item.Price,
			CategoryId = category.Id,
		};

		dbContext.Items.Add(itemEntity);
		dbContext.SaveChanges();

		return Created(
			Url.Action(nameof(GetOne), new { id = itemEntity.Id }),
			itemEntity.ToDto()
		);
	}

	[HttpPut]
	[Route("{id}")]
	public IActionResult Update(int id, [FromBody] ItemDto item)
	{
		var itemToUpdate = dbContext.Items.FirstOrDefault(i => i.Id == id);

		if (itemToUpdate == null)
		{
			return NotFound();
		}

		var category = dbContext.Categories.FirstOrDefault(c => c.Name == item.Category);

		if (category == null)
		{
			return BadRequest(new { Message = $"Category with name {item.Category} does not exist" });
		}

		itemToUpdate.Name = item.Name;
		itemToUpdate.Description = item.Description;
		itemToUpdate.AmountInStorage = item.AmountInStorage;
		itemToUpdate.Price = item.Price;
		itemToUpdate.CategoryId = category.Id;

		dbContext.SaveChanges();

		return NoContent();
	}

	[HttpDelete]
	[Route("{id}")]
	public IActionResult Delete(int id)
	{
		var itemToDelete = dbContext.Items.FirstOrDefault(i => i.Id == id);

		if (itemToDelete == null)
		{
			return NotFound();
		}

		dbContext.Items.Remove(itemToDelete);
		dbContext.SaveChanges();

		return NoContent();
	}
}

public record ItemDto(
	int? Id,
	string Name,
	string? Description,
	int AmountInStorage,
	decimal Price,
	string Category
);

internal static class Extensions
{
	internal static ItemDto ToDto(this Item entity) => new(
		entity.Id,
		entity.Name,
		entity.Description,
		entity.AmountInStorage,
		entity.Price,
		entity.Category!.Name
	);
}

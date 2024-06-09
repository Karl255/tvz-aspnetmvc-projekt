using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("api/cart")]
[ApiController]
[Authorize]
public class CartApiController(StoreDbContext dbContext, UserManager<User> userManager) : ControllerBase
{
	[HttpPost]
	[Route("items")]
	public async Task<IActionResult> AddOne(CartItemRequest request)
	{
		var user = await GetUser();
		var order = dbContext.Orders.FirstOrDefault(o => o.UserId == user.Id && o.Status == OrderStatus.Cart);

		if (order == null)
		{
			order = dbContext.Orders.Add(new Order
			{
				Status = OrderStatus.Cart,
				UserId = user.Id,
			}).Entity;

			dbContext.SaveChanges();
		}

		var orderItem = dbContext.OrderItems.FirstOrDefault(oi => oi.OrderId == order.Id && oi.ItemId == request.ItemId);

		if (orderItem == null)
		{
			dbContext.OrderItems.Add(new OrderItem
			{
				OrderId = order.Id,
				ItemId = request.ItemId,
				Amount = 1,
			});
		}
		else
		{
			orderItem.Amount++;
		}

		dbContext.SaveChanges();

		return NoContent();
	}
	[HttpDelete]
	[Route("items")]
	public async Task<IActionResult> RemoveOne(CartItemRequest request)
	{
		var user = await GetUser();
		var order = dbContext.Orders.FirstOrDefault(o => o.UserId == user.Id && o.Status == OrderStatus.Cart);

		if (order == null)
		{
			return NoContent();
		}

		var orderItem = dbContext.OrderItems.FirstOrDefault(oi => oi.OrderId == order.Id && oi.ItemId == request.ItemId);

		if (orderItem == null)
		{
			return NoContent();
		}

		if (orderItem.Amount <= 1)
		{
			dbContext.Remove(orderItem);
		}
		else
		{
			orderItem.Amount--;
		}

		dbContext.SaveChanges();

		return NoContent();
	}

	private async Task<User> GetUser() => (await userManager.GetUserAsync(User))!;

	public record CartItemRequest(
		[property: JsonPropertyName("itemId")] int ItemId
	);
}

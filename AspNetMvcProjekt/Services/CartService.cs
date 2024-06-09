using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using AspNetMvcProjekt.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AspNetMvcProjekt.Web.Services;

public class CartService(
	StoreDbContext dbContext,
	UserManager<User> userManager
)
{
	public async Task<List<OrderItem>> GetItems(ClaimsPrincipal user)
	{
		return (await GetOrderItems(user))
			.Include(oi => oi.Item)
			.ToList();
	}

	public async Task<int> GetItemsCount(ClaimsPrincipal user) => (await GetOrderItems(user)).Sum(oi => oi.Amount);

	public async Task<bool> PlaceOrder(ClaimsPrincipal user, PlaceOrderModel completeOrder)
	{
		var loggedInUser = await userManager.GetUserAsync(user);

		if (loggedInUser == null)
		{
			return false;
		}

		var order = dbContext.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.User == loggedInUser && o.Status == OrderStatus.Cart);

		if (order == null || order.OrderItems.Count == 0)
		{
			return false;
		}

		order.Address = completeOrder.Address;
		order.City = completeOrder.City;
		order.ZipCode = completeOrder.ZipCode;
		order.Status = OrderStatus.Ordered;

		dbContext.SaveChanges();

		return true;
	}

	private async Task<IQueryable<OrderItem>> GetOrderItems(ClaimsPrincipal user)
	{
		var loggedInUser = await userManager.GetUserAsync(user);

		if (loggedInUser == null)
		{
			return Empty();
		}

		var cart = dbContext.Orders
			.FirstOrDefault(o => o.UserId == loggedInUser.Id && o.Status == OrderStatus.Cart);

		return cart == null ? Empty() : dbContext.OrderItems.Where(oi => oi.OrderId == cart.Id);

		IQueryable<OrderItem> Empty() => Enumerable.Empty<OrderItem>().AsQueryable();
	}
}

using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
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

	private async Task<IQueryable<OrderItem>> GetOrderItems(ClaimsPrincipal user)
	{
		var loggedInUser = await userManager.GetUserAsync(user);

		var cart = dbContext.Orders
			.FirstOrDefault(o => o.Status == OrderStatus.Cart);

		return cart == null ? Enumerable.Empty<OrderItem>().AsQueryable() : dbContext.OrderItems.Where(oi => oi.OrderId == cart.Id);
	}
}

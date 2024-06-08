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
	public async Task<int> GetItemsInCart(ClaimsPrincipal user)
	{
		var loggedInUser = await userManager.GetUserAsync(user);

		var cart = dbContext.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Status == OrderStatus.Cart);

		if (cart == null)
		{
			return 0;
		}

		return cart.OrderItems.Sum(oi => oi.Amount);
	}
}

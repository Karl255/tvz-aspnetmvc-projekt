using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("manage/orders")]
public class OrderManagementController(
	UserManager<User> userManager,
	StoreDbContext dbContext
) : BaseController(userManager)
{
	public IActionResult Index()
	{
		var orders = dbContext.Orders.Where(o => o.Status != OrderStatus.Cart).Include(o => o.User).ToList();
		return View(orders);
	}

	[Route("{id}")]
	public IActionResult Ship(int id)
	{
		var order = dbContext.Orders.FirstOrDefault(o => o.Id == id && o.Status == OrderStatus.Ordered);

		if (order == null)
		{
			return NotFound();
		}

		order.Status = OrderStatus.Shipped;
		dbContext.SaveChanges();

		return RedirectToAction(nameof(Index));
	}
}

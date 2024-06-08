using AspNetMvcProjekt.Model;
using AspNetMvcProjekt.Web.Models;
using AspNetMvcProjekt.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("cart")]
[Authorize]
public class CartController(
    UserManager<User> userManager,
    CartService cartService
) : BaseController(userManager)
{
	public async Task<IActionResult> Index()
    {
        return View(await cartService.GetItems(User));
    }

    [HttpPost]
    public async Task<IActionResult> Order(PlaceOrderModel placeOrder)
    {
        if (await cartService.PlaceOrder(User, placeOrder))
        {
            return View("OrderPlaced");
        }

        return RedirectToAction(nameof(Index));
    }
}

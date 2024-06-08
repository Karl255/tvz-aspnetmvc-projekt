using AspNetMvcProjekt.Model;
using AspNetMvcProjekt.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("cart")]
public class CartController(
    UserManager<User> userManager,
    CartService cartService
) : BaseController(userManager)
{
	public async Task<IActionResult> Index()
    {
        return View(await cartService.GetItems(User));
    }
}

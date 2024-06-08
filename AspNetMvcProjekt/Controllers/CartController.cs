using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcProjekt.Web.Controllers;

[Route("cart")]
public class CartController(UserManager<User> userManager) : BaseController(userManager)
{
	public IActionResult Index()
    {
        return View();
    }
}

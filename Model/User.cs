using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;

namespace Model;

public class User : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}

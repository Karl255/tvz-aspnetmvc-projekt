using Microsoft.EntityFrameworkCore;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model;
using Microsoft.AspNetCore.Identity;

namespace AspNetMvcProjekt.DAL;

public class StoreDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected StoreDbContext() { }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
}

using Microsoft.EntityFrameworkCore;
using AspNetMvcProjekt.Model;

namespace AspNetMvcProjekt.DAL;

public class StoreDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected StoreDbContext() { }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
}

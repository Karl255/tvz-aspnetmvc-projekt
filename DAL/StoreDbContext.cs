using Microsoft.EntityFrameworkCore;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AspNetMvcProjekt.DAL;

public class StoreDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected StoreDbContext() { }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Passive" },
            new Category { Id = 2, Name = "Active" },
            new Category { Id = 3, Name = "IC" }
        );
	}
}

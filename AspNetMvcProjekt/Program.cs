using AspNetMvcProjekt.DAL;
using AspNetMvcProjekt.Model;
using AspNetMvcProjekt.Web.Controllers;
using AspNetMvcProjekt.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("StoreDbContext"),
        opt => opt.MigrationsAssembly("DAL")
    )
);

builder.Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<StoreDbContext>();

builder.Services.Configure<IdentityOptions>(config =>
{
    config.Password.RequiredUniqueChars = 0;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;
    config.Password.RequireDigit = false;
});

builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/dev/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "category",
    pattern: "category/{name}",
    defaults: new
    {
        controller = nameof(ItemListController).Replace("Controller", ""),
        action = nameof(ItemListController.Category),
    }
);

app.MapControllerRoute(
    name: "item management",
    pattern: "manage/items/{action=Index}/{id?}",
    defaults: new
    {
        controller = nameof(ItemManagementController).Replace("Controller", ""),
    }
);

app.MapRazorPages();

app.Run();

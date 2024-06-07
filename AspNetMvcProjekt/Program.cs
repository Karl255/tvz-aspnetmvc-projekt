using AspNetMvcProjekt.DAL;
using Microsoft.EntityFrameworkCore;
using AspNetMvcProjekt.Model;
using Microsoft.AspNetCore.Identity;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Test/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Unauthorized}");

app.MapRazorPages();

app.Run();

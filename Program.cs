using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Twilite.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

// Assigning ConnectionStrings:DatabaseConnection key-value pair stored in secrets.json
var DatabaseConnection = builder.Configuration["ConnectionStrings:DatabaseConnection"];

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(DatabaseConnection));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Accessing the ConnectionStrings:DatabaseConnection key-value pair and providing it to AppDbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(DatabaseConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

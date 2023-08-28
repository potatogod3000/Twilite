using Twilite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Assigning ConnectionStrings:DatabaseConnection key-value pair stored in secrets.json
var DatabaseConnection = builder.Configuration["ConnectionStrings:DatabaseConnection"];

// Add services to the container.
builder.Services.AddControllersWithViews();

// Accessing the ConnectionStrings:DatabaseConnection key-value pair and providing it to AppDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(DatabaseConnection));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

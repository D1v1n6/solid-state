using Microsoft.EntityFrameworkCore;
using SolidCoreMvc.Data;
using SolidCoreMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MVC
builder.Services.AddControllersWithViews();

// Register DbContext
var home = Environment.GetEnvironmentVariable("HOME") 
           ?? Environment.GetEnvironmentVariable("HOME_DIR") 
           ?? "/home";

var dbPath = Path.Combine(home, "SolidCoreMvc.db");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));
// Register ProductService
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();

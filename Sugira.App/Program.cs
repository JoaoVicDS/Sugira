using Microsoft.EntityFrameworkCore;
using Sugira.Data;
using Sugira.Services;
using Sugira.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Get Connection String from configuration
var connectionString = builder.Configuration
    .GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection String 'ApplicationDbContextConnection' not found.");

// Add new dbcontext service with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IMenuService, MenuService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

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
    pattern: "{controller=Menu}/{action=Index}/{id?}");

app.Run();

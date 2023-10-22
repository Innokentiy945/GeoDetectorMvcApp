using GeoDetectorMvcApp.Context;
using GeoDetectorWebApi.Context;
using Microsoft.EntityFrameworkCore;
using WeatherServiceWebApi.Context;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<GeoContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<WeatherContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<CombinedContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

//app.UseMiddleware<CustomMiddlewareAuth>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

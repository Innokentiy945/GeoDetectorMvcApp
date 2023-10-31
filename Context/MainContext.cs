using CombinedServiceWebApi.Model;
using GeoDetectorServiceWebApi.Model;
using Microsoft.EntityFrameworkCore;
using WeatherServiceWebApi.Model;

namespace GeoDetectorMvcApp.Context;

public class MainContext : DbContext
{
    public DbSet<GeoModel> Geo { get; set; } = null!; 
    public DbSet<WeatherModel> Weather { get; set; } = null!;
    public DbSet<CombinedModel> Combined { get; set; } = null!;
    
    public MainContext(DbContextOptions<MainContext> options) : base(options) { }
}
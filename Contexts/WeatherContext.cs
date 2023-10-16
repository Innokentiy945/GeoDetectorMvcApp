using GeoDetectorMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoDetectorMvcApp.DbContext
{
    public class WeatherContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }
        public DbSet<WeatherModel> Weather { get; set; }
    }
}

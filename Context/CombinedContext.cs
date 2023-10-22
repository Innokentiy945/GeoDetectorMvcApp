using GeoDetectorMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoDetectorMvcApp.Context;

public class CombinedContext : DbContext
{
    public CombinedContext(DbContextOptions<CombinedContext> options) : base(options) { }
    public DbSet<CombinedModel> Combined { get; set; }
}
﻿using GeoDetectorMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoDetectorMvcApp.DbContext
{
    public class GeoContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public GeoContext(DbContextOptions options) : base(options) { }
        public DbSet<GeoModel> Geo { get; set; }
    }
}

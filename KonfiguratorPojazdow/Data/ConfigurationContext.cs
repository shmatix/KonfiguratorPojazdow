using Microsoft.EntityFrameworkCore;
using System;

namespace KonfiguratorPojazdow.Data
{
    public class ConfigurationContext : DbContext
    {
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Paint> Paints { get; set; }

        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

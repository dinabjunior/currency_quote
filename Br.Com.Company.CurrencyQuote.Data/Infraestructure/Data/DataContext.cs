using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Br.Com.Company.CurrencyQuote.Data.Infraestructure.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        internal DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
        {
            _configuration = configuration;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("global"), pgOptions =>
            {
                pgOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
            }).UseSnakeCaseNamingConvention();

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureSegmentRateTable();

            base.OnModelCreating(modelBuilder);
        }
    }
}

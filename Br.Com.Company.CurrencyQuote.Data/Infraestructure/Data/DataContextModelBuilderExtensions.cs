using Br.Com.Company.CurrencyQuote.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Br.Com.Company.CurrencyQuote.Data.Infraestructure.Data
{
    internal static class DataContextModelBuilderExtensions
    {
        internal static void ConfigureSegmentRateTable(this ModelBuilder mb)
        {
            var builder = mb.Entity<SegmentRate>();
            builder.ToTable("segment_rate", schema: "dbo");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Rate).HasPrecision(18,4).IsRequired();
            builder.Property(c => c.Segment).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired().HasDefaultValueSql("now()");

            builder.HasIndex(c => c.Segment).IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using BettingWorld.Assessment.Ishe.API.Models;

namespace BettingWorld.Assessment.Ishe.API.Data
{
    public class RatesContext:DbContext
    {

        public DbSet<CurrencyRatesHistory> CurrencyRatesHistory { get; set; }

        public RatesContext()
        {
            
        }

        public RatesContext(DbContextOptions<RatesContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = "Server=rates.mysql;Port=3333;Database=ExchangeRates;Uid=dbuser;Pwd=password";
                optionsBuilder
                    .EnableSensitiveDataLogging(false)
                    .UseMySQL(connString, options => options.MaxBatchSize(150));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrencyRatesHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.Rates).IsRequired();
            });
        }
    }
}

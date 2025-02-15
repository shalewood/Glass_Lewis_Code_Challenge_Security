using CompanyApi.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Api.Data
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
                
        }
        public virtual DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add your own configuration here
            CreateTable(modelBuilder);
            SeedDataBase(modelBuilder);
            base.OnModelCreating(modelBuilder); // Need this for Identity
        }

        private void CreateTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                //Primary Key
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Isin).IsUnique();// Ensure ISIN is unique

                entity.Property(e => e.Exchange).IsRequired();
                entity.Property(e => e.Isin).HasMaxLength(12).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Ticker).IsRequired();
                entity.Property(e => e.Website);
            });
        }
        private void SeedDataBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
            new Company
                {
                    Id = 1,
                    Name = "Apple Inc.",
                    Exchange = "NASDAQ",
                    Ticker = "AAPL",
                    Isin = "US0378331005",
                    Website = "http://www.apple.com"
                },
                new Company
                {
                    Id = 2,
                    Name = "British Airways Plc",
                    Exchange = "Pink Sheets",
                    Ticker = "US1104193065",
                    Isin = "US1104193065"
                },
                new Company
                {
                    Id = 3,
                    Name = "Heineken NV",
                    Exchange = "Euronext Amsterdam",
                    Ticker = "HEIA",
                    Isin = "NL0000009165"
                },
                new Company
                {
                    Id = 4,
                    Name = "Panasonic Corp",
                    Exchange = "Tokyo Stock Exchange",
                    Ticker = "6752",
                    Isin = "JP3866800000",
                    Website = "http://www.panasonic.co.jp"
                },
                new Company
                {
                    Id = 5,
                    Name = "Porsche Automobil",
                    Exchange = "Deutsche Börse",
                    Ticker = "PAH3",
                    Isin = "DE000PAH0038",
                    Website = "https://www.porsche.com/"
                }
            );
        }
    }
}

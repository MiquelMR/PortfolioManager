using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioAsset> PortfolioAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioAsset>()
                .HasKey(pa => new { pa.PortfolioId, pa.AssetId });
            modelBuilder.Ignore<AssetDto>(); // Le indica a EF Core que ignore esta clase por completo
            base.OnModelCreating(modelBuilder);
        }
    }
}

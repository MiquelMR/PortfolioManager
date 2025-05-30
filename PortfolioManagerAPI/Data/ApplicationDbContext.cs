﻿using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialAsset> Assets { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioAsset> PortfolioAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioAsset>()
                .HasKey(pa => new { pa.PortfolioId, pa.AssetId });
            modelBuilder.Ignore<FinancialAssetDto>();
            base.OnModelCreating(modelBuilder);
        }
    }
}

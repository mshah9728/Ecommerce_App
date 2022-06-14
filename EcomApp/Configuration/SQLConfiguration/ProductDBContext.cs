using System;
using EcomApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace EcomApp.Configuration.SQLConfiguration
{
    public partial class ProductDBContext : DbContext
    {
        private IConfiguration _configuration;
        public ProductDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:ProductDB"]);
            }
        }
        public virtual DbSet<product_Model> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<product_Model>(entity =>
            {
                entity.ToTable("tbl_Product");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

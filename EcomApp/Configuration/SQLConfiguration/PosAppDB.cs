using System;
using EcomApp.Models.Customer;
using EcomApp.Models.Product;
using EcomApp.Models.Supplier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace EcomApp.Configuration.SQLConfiguration
{
    public partial class PosAppDB : DbContext
    {
        private IConfiguration _configuration;
        public PosAppDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PosAppDB(DbContextOptions<PosAppDB> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:PosAppDB"]);
            }
        }
        public virtual DbSet<product_Model> Products { get; set; }
        public virtual DbSet<Product_CategoryModel> Product_Categories { get; set; }
        public virtual DbSet<Product_LocationModel> Product_Locations { get; set; }
        public virtual DbSet<Supplier_Model> Supplier { get; set; }
        public virtual DbSet<Customer_Model> Customer { get; set; }
        public virtual DbSet<Customer_Billing_Detail_Model>  Customer_Billing_Details {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<product_Model>(entity =>
            {
                entity.ToTable("tbl_Product");
                entity.HasKey(p => p.Product_id);
            });
            modelBuilder.Entity<Product_LocationModel>(entity =>
            {
                entity.ToTable("tbl_Product_Location");
                entity.HasKey(p => p.P_L_ID);
            });
            modelBuilder.Entity<Product_CategoryModel>(entity =>
            {
                entity.ToTable("tbl_Product_Category");
                entity.HasKey(p => p.P_Category_Id);
            });
            modelBuilder.Entity<Supplier_Model>(entity =>
            {
                entity.ToTable("tbl_Supplier");
                entity.HasKey(p => p.Supplier_ID);
            });
            modelBuilder.Entity<Customer_Billing_Detail_Model>(entity =>
            {
                entity.ToTable("tbl_Customer_Billing_Detail");
                entity.HasKey(p => p.C_BillingDetail_ID);
            });
            modelBuilder.Entity<Customer_Model>(entity =>
            {
                entity.ToTable("tbl_Customer");
                entity.HasKey(p => p.Customer_ID);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

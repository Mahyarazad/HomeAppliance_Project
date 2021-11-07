using System;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain;
using SM.Domain.ProductAgg;
using SM.Domain.ProductPictureAgg;
using SM.Domain.SliderAgg;
using SM.Infrastructure.Mapping;
using static Microsoft.EntityFrameworkCore.DbContext;

namespace SM.Infrastructure
{
    public class SMContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public SMContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

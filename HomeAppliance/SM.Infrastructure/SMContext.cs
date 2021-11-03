using System;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain;
using SM.Infrastructure.Mapping;
using static Microsoft.EntityFrameworkCore.DbContext;

namespace SM.Infrastructure
{
    public class SMContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
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

using System;
using IM.Domain;
using IM.Infrastructure.IMMapping;
using Microsoft.EntityFrameworkCore;

namespace IM.Infrastructure
{
    public class IMContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public IMContext(DbContextOptions<IMContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

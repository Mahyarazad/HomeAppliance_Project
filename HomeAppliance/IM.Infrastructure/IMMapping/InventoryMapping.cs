using IM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IM.Infrastructure.IMMapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory", schema: "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Count).IsRequired();

            builder.OwnsMany(x => x.InventoryOperations, ModelBuilder =>
            {
                ModelBuilder.ToTable("Operations");
                ModelBuilder.HasKey(x => x.Id);
                ModelBuilder.Property(x => x.Count).IsRequired();
                ModelBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
                ModelBuilder.Property(x => x.Description).HasMaxLength(1000);
            });

        }
    }
}
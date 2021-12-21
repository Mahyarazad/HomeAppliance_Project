using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.OrderAgg;

namespace SM.Infrastructure.Mapping
{
    public interface OrderMapping : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", schema: "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AcoountId).HasMaxLength(5).IsRequired();
            builder.OwnsMany(x => x.OrderItems, ModelBuilder =>
            {
                ModelBuilder.ToTable("OrderItems");
                ModelBuilder.HasKey(x => x.Id);
                ModelBuilder.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
            });
        }
    }
}
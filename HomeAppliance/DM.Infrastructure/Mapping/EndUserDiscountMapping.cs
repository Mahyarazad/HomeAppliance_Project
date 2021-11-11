using DM.Domian;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace DM.Infrastructure.Mapping
{
    public class EndUserDiscountMapping : IEntityTypeConfiguration<EndUserDiscount>
    {
        public void Configure(EntityTypeBuilder<EndUserDiscount> builder)
        {
            builder.ToTable("EndUserDiscount", schema: "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Occasion).HasMaxLength(500);
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
        }
    }
}
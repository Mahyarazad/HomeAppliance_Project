using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductAgg;

namespace SM.Infrastructure.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", schema: "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Keyword).IsRequired().HasMaxLength(500);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ShortDescription).HasMaxLength(200);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.Pictures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

            builder.HasMany(x => x.Comments).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
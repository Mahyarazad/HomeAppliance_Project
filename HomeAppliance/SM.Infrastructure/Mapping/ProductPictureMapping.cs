using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductPictureAgg;

namespace SM.Infrastructure.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPicture", schema: "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Pictures).HasForeignKey(x => x.ProductId);
        }
    }
}
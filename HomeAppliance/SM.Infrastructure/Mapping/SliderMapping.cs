using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.SliderAgg;

namespace SM.Infrastructure.Mapping
{
    public class SliderMapping : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.ToTable("Slider", schema: "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(600);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(600);

            builder.Property(x => x.Heading).HasMaxLength(255);
            builder.Property(x => x.Title).HasMaxLength(255);
            builder.Property(x => x.Text).HasMaxLength(255);
            builder.Property(x => x.BtnText).HasMaxLength(50);
            builder.Property(x => x.BtnText).HasMaxLength(500);
        }
    }
}